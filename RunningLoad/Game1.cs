// このファイルで必要なライブラリのnamespaceを指定
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RunningLoad.Actor;
using RunningLoad.Def;
using RunningLoad.Device;
using RunningLoad.Scene;
using System.Collections.Generic;

/// <summary>
/// プロジェクト名がnamespaceとなります
/// </summary>
namespace RunningLoad
{
    /// <summary>
    /// ゲームの基盤となるメインのクラス
    /// 親クラスはXNA.FrameworkのGameクラス
    /// </summary>
    public class Game1 : Game
    {
        // フィールド（このクラスの情報を記述）
        private GraphicsDeviceManager graphicsDeviceManager;//グラフィックスデバイスを管理するオブジェクト
        //private SpriteBatch spriteBatch;//画像をスクリーン上に描画するためのオブジェクト(Rendererクラスに委託）
        private Renderer renderer;//画像描画クラス

        private DeviceManager deviceManager;//デバイス関連管理者
        private SceneManager sceneManager;//シーン管理者
        private SoundManager soundManager;//サウンド管理者
        #region シーン管理者へ委託
        //private GameScene gameScene;//ゲームシーンクラス（後ほどシーン管理者に委託予定）
        //private Result resultScene;//リザルト画面
        #endregion
        #region ScrollCameraクラスに委託
        //private float scroll = 0;//ステージスクロールの定数
        //private Vector2 stagePos1, stagePos2, stagePos3, stagePos4;//ステージ描画位置
        #endregion
        #region Rendererクラスにより不要に
        //画像用変数
        //private Texture2D textureBlack;
        //private Texture2D textureWhite;
        //private Texture2D textureStage;
        #endregion
        #region Runnerクラスに委託
        ////キー入力関連
        //private Vector2 position;//位置
        //private Vector2 movement;//移動量
        #endregion
        #region GamePlayクラスに委託
        //キャラクター関連
        //private ScrollCamera camera;//スクロール用カメラ
        //private Runner runner;//プレイヤー
        //private FixedEnemy enemy;//固定エネミー
        //private FlyingEnemy flyingEnemy;//飛行型エネミー
        #endregion

        /// <summary>
        /// コンストラクタ
        /// （new で実体生成された際、一番最初に一回呼び出される）
        /// </summary>
        public Game1()
        {
            //グラフィックスデバイス管理者の実体生成
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            //コンテンツデータ（リソースデータ）のルートフォルダは"Contentに設定
            Content.RootDirectory = "Content";

            //画面サイズを設定
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height;
            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;

            Window.Title = "恐竜Run";
        }

        /// <summary>
        /// 初期化処理（起動時、コンストラクタの後に1度だけ呼ばれる）
        /// </summary>
        protected override void Initialize()
        {
            // この下にロジックを記述
            deviceManager = DeviceManager.CreateInstance(Content, GraphicsDevice);
            //Content→Game1のフィールド参照 
            //GraphicsDevice→Game1の抽象クラスGameクラスの公開フィールドを参照
            renderer = deviceManager.GetRenderer();
            soundManager = deviceManager.GetSound();
            #region デバイス管理者を経由した生成に変更
            //renderer = new Renderer(Content, GraphicsDevice);
              //Content→ContentManagerクラスのオブジェクト（Gameクラスが所有）
              //GraphicsDevice→GraphicsDeviceクラスのオブジェクト（上に同じ）
            //soundManager = new SoundManager(Content);
            #endregion
            #region ステージの描画位置用の変数(scrollCameraクラスに委託）
            //stagePos1 = new Vector2(0, 500);
            //stagePos2 = new Vector2(700, 500);
            //stagePos3 = new Vector2(1400, 500);
            //stagePos4 = new Vector2(2100, 500);
            //scroll = 10f;
            #endregion
            #region シーン管理者の生成・シーンの追加
            sceneManager = new SceneManager();
            sceneManager.AddScene(EScene.Title, new Title());
            IScene addisionalScene = new GameScene();
            sceneManager.AddScene(EScene.GameScene, addisionalScene);
            sceneManager.AddScene(EScene.Result, new Result(addisionalScene));//これでリザルト画面の背景はゲームシーン（停止状態）になる
            sceneManager.SetScene(EScene.Title);//最初のシーンをタイトルシーンにセット
            #endregion
            #region シーン管理者に委託
            //gameScene = new GameScene();
            //gameScene.Initialize();
            //resultScene = new Result(gameScene);
            #endregion
            #region GamePlayクラスに委託
            //camera = new ScrollCamera("stage");
            //runner = new Runner();
            //runner.Initialize();
            //enemy = new FixedEnemy();
            //enemy.Initialize();
            //flyingEnemy = new FlyingEnemy();
            //flyingEnemy.Initialize();
            #endregion

            // この上にロジックを記述
            base.Initialize();// 親クラスの初期化処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// コンテンツデータ（リソースデータ）の読み込み処理
        /// （起動時、１度だけ呼ばれる）
        /// </summary>
        protected override void LoadContent()
        {
            // 画像を描画するために、スプライトバッチオブジェクトの実体生成
            //spriteBatch = new SpriteBatch(GraphicsDevice);→Rendererクラスオブジェクトに委託

            // この下にロジックを記述
            #region 画像リソース
            renderer.LoadContent("stage");
            renderer.LoadContent("bluesky_cloudy");
            renderer.LoadContent("number");
            renderer.LoadContent("score");

            //シーンごとの背景
            renderer.LoadContent("ending");
            renderer.LoadContent("title");

            //使用キャラクター
            //ティラノサウルス関連
            renderer.LoadContent("dinasour_nikushoku");
            renderer.LoadContent("dinasour_running");
            renderer.LoadContent("dinasour_squat");
            renderer.LoadContent("dinasour_cry");
            //プテラノドン関連
            renderer.LoadContent("puteranodon");
            renderer.LoadContent("puteranodon_flying");
            //サボテン関連
            renderer.LoadContent("saboten");
            renderer.LoadContent("saboten_flower");
            #endregion

            #region BGM（MP3）リソース
            string filepath_BGM = "./BGM/";

            soundManager.LoadBGM("endingBGM", filepath_BGM);
            soundManager.LoadBGM("titleBGM", filepath_BGM);
            soundManager.LoadBGM("playBGM", filepath_BGM);
            #endregion

            #region SE(WAV)リソース
            string filepath_SE = "./SE/";

            soundManager.LoadSE("dora"        , filepath_SE);
            soundManager.LoadSE("middle_punch", filepath_SE);
            soundManager.LoadSE("punyu"       , filepath_SE);
            soundManager.LoadSE("select_menu" , filepath_SE);
            soundManager.LoadSE("wadaiko1"    , filepath_SE);
            soundManager.LoadSE("wadaiko2"    , filepath_SE);
            #endregion

            #region ピクセル関連
            //色を格納した配列(読み込むピクセルの縦横の乗算の個数分の要素が必要）
            Color[] hitArea_Runner = new Color[1 * 1]{
                Color. Red   };//Runner用
            Color[] hitArea_Fix = new Color[1 * 1] {
                Color.Blue     };//Fix用
            Color[] hitArea_Fly = new Color[1 * 1] {
                Color.Green };//Fly用

            //1 * 1ピクセルを生成(この段階ではデータができただけで視認はできない
            //→SetDataメソッドを使い色データを紐づける
            Texture2D hitArea1 = new Texture2D(GraphicsDevice, 1, 1);
            Texture2D hitArea2 = new Texture2D(GraphicsDevice, 1, 1);
            Texture2D hitArea3 = new Texture2D(GraphicsDevice, 1, 1);

            //色データとの紐づけ
            hitArea1.SetData(hitArea_Runner);
            hitArea2.SetData(hitArea_Fix);
            hitArea3.SetData(hitArea_Fly);

            //テクスチャデータを読み込み
            renderer.LoadContent("hitArea_R", hitArea1);//hitAreaという名前でhitAreaという変数名のTexture2D型オブジェクトを登録
            renderer.LoadContent("hitArea_Fix", hitArea2);
            renderer.LoadContent("hitArea_Fly", hitArea3);
            #endregion
            #region Rendererオブジェクトを用いてリファクタリング
            //textureWhite = Content.Load<Texture2D>("white");
            //textureBlack = Content.Load<Texture2D>("black");
            //textureStage = Content.Load<Texture2D>("stage");
            #endregion

            // この上にロジックを記述
        }

        /// <summary>
        /// コンテンツの解放処理
        /// （コンテンツ管理者以外で読み込んだコンテンツデータを解放）
        /// </summary>
        protected override void UnloadContent()
        {
            // この下にロジックを記述


            // この上にロジックを記述
        }

        /// <summary>
        /// 更新処理
        /// （1/60秒の１フレーム分の更新内容を記述。音再生はここで行う）
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Update(GameTime gameTime)
        {
            // ゲーム終了処理（ゲームパッドのBackボタンかキーボードのエスケープボタンが押されたら終了）
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                 (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                Exit();
            }

            // この下に更新ロジックを記述
            Input.Update();
            sceneManager.Update(gameTime);
            #region GamePlayクラスに委託
            //enemy.Update(gameTime);
            //runner.Update(gameTime);
            //flyingEnemy.Update(gameTime);

            //if (runner.IsHit(enemy))
            //{
            //    runner.HitAction();
            //    enemy.HitAction();
            //}
            //if (runner.IsHit(flyingEnemy))
            //{
            //    runner.HitAction();
            //    flyingEnemy.HitAction();
            //}
            //if (runner.IsDead())
            //{
            //    Exit();
            //}
            //camera.Update(gameTime);
            #endregion
            #region ステージの描画位置をずらす（Cameraに委託）
            //stagePos1.X -= scroll;
            //stagePos2.X -= scroll;
            //stagePos3.X -= scroll;
            //stagePos4.X -= scroll;
            //if (stagePos1.X < -700) stagePos1.X = 2100;
            //else if (stagePos2.X < -700) stagePos2.X = 2100;
            //else if (stagePos3.X < -700) stagePos3.X = 2100;
            //else if (stagePos4.X < -700) stagePos4.X = 2100;
            #endregion
            #region シーン管理者に委託
            //if (resultScene.IsEnd())
            //{
            //    gameScene.Update(gameTime);
            //}
            //if (gameScene.IsEnd())
            //{
            //    resultScene.Update(gameTime);
            //}
            #endregion
            #region Runnerクラスに委託
            ////毎フレーム移動量を初期化
            //movement = Vector2.Zero;

            ////キー入力
            ////右を入力
            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //{
            //    movement.X += 1.0f;
            //}
            ////左を入力
            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //{
            //    movement.X -= 1.0f;
            //}
            ////上を入力
            //if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //{
            //    movement.Y -= 1.0f;
            //}
            ////下を入力
            //if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //{
            //    movement.Y += 1.0f;
            //}

            ////移動量の正規化
            //if(movement.Length() != 0)//Lengthメソッド（指定されたVector2型の長さを返却）
            //{
            //    movement.Normalize();
            //}

            ////初期化時の座標に移動量を加算
            //float speed = 5.0f;
            //position = position + movement * speed;//変数speedの分だけ座標の変化具合がさらに変わる
            #endregion
            // この上にロジックを記述
            base.Update(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Draw(GameTime gameTime)
        {
            // 画面クリア時の色を設定
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // この下に描画ロジックを記述
            #region 描画開始・終了処理の削除（各シーンの描画処理に委託）
            //SpriteBatchのBeginメソッドとEndメソッドは二つで1セット
            //SpriteBatchのBeginメソッドが発動中はEndメソッドが来るまで再びBeginメソッドを使うことはできない
            //シーンごとにRendererのBeginとEndを使うため、Game1では必要ないため削除
            //renderer.Begin();
            //renderer.End();
            #endregion
            sceneManager.Draw(renderer);//ここのDrawメソッドの
            #region　GamePlayクラスに委託
            //camera.Draw(renderer);
            //enemy.Draw(renderer);
            //flyingEnemy.Draw(renderer);
            //runner.Draw(renderer);
            #endregion
            #region 不要になった描画処理
            //renderer.DrawTexture("stage", stagePos1);//ステージ1枚目
            //renderer.DrawTexture("stage", stagePos2);//ステージ２枚目
            //renderer.DrawTexture("stage", stagePos3);//ステージ3枚目
            //renderer.DrawTexture("stage", stagePos4);//ステージ4枚目
            //renderer.DrawTexture("black", new Vector2(250, 220));
            //renderer.DrawTexture("white", position);
            #endregion
            #region シーン管理者に委託
            //if (resultScene.IsEnd())
            //{
            //    gameScene.Draw(renderer);
            //}
            //if (gameScene.IsEnd())
            //{
            //    resultScene.Draw(renderer);
            //}
            #endregion

            #region Rendererクラスに委託
            //spriteBatch.Begin();

            //spriteBatch.Draw(textureStage, Vector2.Zero, Color.White);
            //spriteBatch.Draw(textureWhite, position, Color.White);
            //spriteBatch.Draw(textureBlack, new Vector2(250, 220), Color.White);

            //spriteBatch.End();
            #endregion

            //この上にロジックを記述
            base.Draw(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }
    }
}
