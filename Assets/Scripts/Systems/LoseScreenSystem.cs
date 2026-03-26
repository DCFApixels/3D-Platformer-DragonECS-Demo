using DCFApixels.DragonECS;

namespace Platformer
{
    public class LoseScreenSystem : IEcsDestroy, IEcsInit, IEcsInject<GameData>
    {
        GameData _gameData;
        public void Init()
        {
            _gameData.S.UI.LoseScreenRestartButton.onClick.AddListener(OnRestarClicked);
        }
        private void OnRestarClicked()
        {
            _gameData.S.ReloadScene();
        }
        public void Destroy()
        {
            _gameData.S.UI.LoseScreenRestartButton.onClick.RemoveAllListeners();
        }

        public void Inject(GameData obj) => _gameData = obj;
    }

}
