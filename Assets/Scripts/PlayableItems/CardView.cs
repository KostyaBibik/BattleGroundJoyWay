using System;
using Systems;
using Db;
using Db.Actions;
using Db.Actions.Impl;
using Db.Enums;
using Db.Impl;
using Game;
using Game.Components;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace PlayableItems
{
    public class CardView :
        MonoBehaviour, 
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IDropHandler,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        public CanvasGroup canvasGroup;
        
        [SerializeField] private TMP_Text health;
        [SerializeField] private TMP_Text name;
        [SerializeField] private Outline outline;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color onPointerEnterColor;
        [SerializeField] private TemporaryHealthView temporaryHealthView;
        [SerializeField] private Image actionIcon;
        
        public CanvasGroup CanvasGroup => canvasGroup;
        public HealthComponent HealthComponent => _healthComponent;
        public Image ActionIcon => actionIcon;
        public IActionState ActionState;
        public event Action onEndMove;
        
        private CardMovingSystem _movingSystem;
        private IActionSettings _actionSettings;
        private SignalBus _signalBus;
        private CardInfo _cardInfo = new CardInfo();

        private HealthComponent _healthComponent;
        
        private bool _readyMoving;
        private bool _onMovingTeam;
        private bool _isMoving;

        [Inject]
        public void Construct(
            CardMovingSystem movingSystem, 
            ActionSettings actionSettings,
            SignalBus signalBus
        )
        {
            _movingSystem = movingSystem;
            _actionSettings = actionSettings;
            _signalBus = signalBus;
        }

        public CardView Initialize(CardVo cardVo)
        {
            _cardInfo.team = cardVo.Team;
            
            _healthComponent = new HealthComponent(health, temporaryHealthView, this);
            _healthComponent.SetHealth(cardVo.health);
            _healthComponent.onDie += delegate { Destroy(); };
            SetName(cardVo.name);
            
            return this;
        }

        private void Start()
        {
            _signalBus.Subscribe<EndMotionSignal>(OnCatchEndMotionSignal);
        }

        public void SetAction(EActionType actionType, IActionState actionState)
        {
            ActionState = actionState.Initialize(this, _actionSettings.GetAction(actionType));
        }

        public void SetName(string value)
        {
            _cardInfo.name = value;
            name.text = value;
        }

        public ETeam GetTeam()
        {
            return _cardInfo.team;
        }

        public void SwitchActiveState(bool flag)
        {
            _onMovingTeam = _readyMoving = flag;
            outline.enabled = flag;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(!CheckAndSetMoving())
                return;
            
            _movingSystem.OnBeginDrag(eventData, this);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if(!_isMoving)
                return;
            
            _movingSystem.OnDrag(eventData, this);
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            if(!_isMoving)
                return;

            _isMoving = false;
            _movingSystem.OnEndDrag(eventData, this);
        }

        private bool CheckAndSetMoving()
        {
            if (!_readyMoving) 
                return false;

            _isMoving = true;
            _readyMoving = false;
            return true;
        }

        public void OnDrop(PointerEventData eventData)
        {
            _movingSystem.OnDrop(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(_onMovingTeam && _readyMoving)
                outline.enabled = true;

            if (_movingSystem.getSelectedCard)
            {
                outline.enabled = true;
                outline.effectColor = onPointerEnterColor;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(_onMovingTeam && !_readyMoving || !_onMovingTeam)
                outline.enabled = false;
            
            outline.effectColor = selectedColor;
        }

        private void OnCatchEndMotionSignal(EndMotionSignal endMotionSignal)
        {
            onEndMove?.Invoke();
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}