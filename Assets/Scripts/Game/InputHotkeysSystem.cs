using Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public class InputHotkeysSystem : ITickable
    {
        private readonly SignalBus _signalBus;
        
        InputHotkeysSystem(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _signalBus.Fire(new EndMotionSignal());
            }
            else if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}