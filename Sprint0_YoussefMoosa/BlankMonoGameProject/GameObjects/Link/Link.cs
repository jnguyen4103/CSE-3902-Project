namespace Sprint03
{
    public class Link : ILink
    {

        // Basic states that control what action he is doing
        // Idle just means he is not taking damage, attacking or using items.
        // The idle allows for movement
        public enum LinkState
        {
            Attacking,
            UsingSecondary,
            Moving,
            Idle,
            Damaged,
            Dead
        }

        // 4 directional states of Link, these are used to determine which directional
        // sprite to drawn in certain states
        public enum LinkDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        // Creating references to Link's Sprite and StateMachine
        public LinkSprite Sprite;
        public LinkStateMachine LinkSM;

        // Creating a reference to the Game so Link's decorators can
        // have access to link by using monoProcess
        public Game1 Game;

        // Setting initial action and movement states
        public LinkState State = LinkState.Idle;
        public LinkDirection Direction = LinkDirection.Up;
        public int hitpoints;
        public int maxHP = 6;

        LinkStateMachine ILink.StateMachine
        {
            get { return LinkSM; }
            set { }
        }

        public LinkSprite SpriteLink
        {
            get { return Sprite; }
            set { }
        }

        public int HP { get => hitpoints; set => hitpoints = value; }
        public int MaxHP { get => maxHP; set => maxHP = value; }

        public Link(LinkSprite sprite, Game1 game)
        {
            Game = game;
            Sprite = sprite;
            hitpoints = 6;
            State = LinkState.Idle;
            LinkSM = new LinkStateMachine(this);
        }

        public void TakeDamage(int damage, int direction)
        {
            hitpoints -= damage;
            if(hitpoints > 0)
            {
                LinkSM.DamagedState(direction);
            } else
            {
                LinkSM.DeadState();
            }
        }

        public void StopLink()
        {
            LinkSM.IdleState();
        }
        public void Draw()
        {
            SpriteLink.DrawSprite();
        }

        public void Update()
        {
            SpriteLink.Update(State, Direction);
        }

        public LinkState GetState()
        {
            return State;
        }

    }
}