using FDK;

namespace TJAPlayer3
{
    /// <summary>
    /// レーンフラッシュのクラス。
    /// </summary>
    public class LaneFlash : CActivity
    {

        public LaneFlash(CTexture texture, int player)
        {
            Texture = texture;
            Player = player;
            base.bDeactivated = true;
        }

        public void Start()
        {
            Counter = new CCounter(0, 100, 2, TJAPlayer3.Timer);
        }

        public override void On活性化()
        {
            Counter = new CCounter();
            base.On活性化();
        }

        public override void On非活性化()
        {
            Counter = null;
            base.On非活性化();
        }

        public override int OnDraw()
        {
            if (Texture == null || Counter == null) return base.OnDraw();
            if (!Counter.b停止中)
            {
                Counter.tStart();
                if (Counter.bEnded) Counter.tStop();
                int opacity = (((150 - Counter.nCurrentValue) * 255) / 100);
                Texture.Opacity = opacity;
                Texture.t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Lane_Background_X[Player], TJAPlayer3.Skin.Game_Lane_Background_Y[Player]);
            }
            return base.OnDraw();
        }

        private CTexture Texture;
        private CCounter Counter;
        private int Player;
    }
}
