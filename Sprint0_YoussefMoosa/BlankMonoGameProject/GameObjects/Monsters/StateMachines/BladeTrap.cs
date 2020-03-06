using Microsoft.Xna.Framework;
using System;
namespace Sprint03
{
    public class BladeTrapSM : IStateMachine
    {
        private Vector2 StartingPosition;
        private string Corner;
        private string CurrentlyAttacking;

        public BladeTrapSM(Monster BladeTrap, Game1 game)
        {
            self = BladeTrap;
            Game = game;
            StartingPosition = self.Sprite.Position;
            Corner = DetermineCorner();
        }

        public override void SpawnState()
        {
            IdleState();
            Console.WriteLine();
            self.State = Monster.MonsterState.Idle;
            self.Sprite.ChangeSpriteAnimation("BladeTrap");
        }

        public override void IdleState()
        {
            LinkDetect();
        }


        public override void AttackState()
        {
            Console.WriteLine(CurrentlyAttacking);
            if(Corner.StartsWith(CurrentlyAttacking) || Corner.EndsWith(CurrentlyAttacking))
            {
                self.State = Monster.MonsterState.Moving;
                MoveState();
            } else if (Timer > 240)
            {
                Timer++;
            } else
            {
                Timer = 0;
                CurrentlyAttacking = "";
                IdleState();
            }
        }

        public override void MoveState()
        {
            Timer++;

            // Horizontal Move Attack

            if (Timer < 80 && (CurrentlyAttacking.Equals("Upper") || CurrentlyAttacking.Equals("Lower")))
            {
                if (Corner.EndsWith("Right"))
                {
                    self.Sprite.Position.X -= 1f;
                }
                else if (Corner.EndsWith("Left"))
                {
                    self.Sprite.Position.X += 1f;
                }
            }
            // Vertical Move Attack
            else if (Timer < 40 && (CurrentlyAttacking.Equals("Left") || CurrentlyAttacking.Equals("Right")))
            {
                if (Corner.StartsWith("Upper"))
                {
                    self.Sprite.Position.Y += 1f;
                }
                else if (Corner.StartsWith("Lower"))
                {
                    self.Sprite.Position.Y -= 1f;
                }

            }
            // Moving back to starting position
            else
            {
                if (StartingPosition != self.Sprite.Position)
                {
                    if (StartingPosition.X > self.Sprite.Position.X)
                    {
                        self.Sprite.Position.X += 0.5f;
                    }
                    else if (StartingPosition.X < self.Sprite.Position.X)
                    {
                        self.Sprite.Position.X -= 0.5f;
                    }
                    else if (StartingPosition.Y > self.Sprite.Position.Y)
                    {
                        self.Sprite.Position.Y += 0.5f;
                    }
                    else if (StartingPosition.Y < self.Sprite.Position.Y)
                    {
                        self.Sprite.Position.Y -= 0.5f;
                    }
                }
                // When BladeTraps have moved back to their starting positions then reset
                else
                {
                    CurrentlyAttacking = "";
                    Timer = 0;
                    IdleState();
                }
            }


        }

        public override void DamagedState(int directionDamaged)
        {
            // Cannot be damaged
        }

        private void LinkDetect()
        {
            Vector2 LP = Game.Link.SpriteLink.Position;

            // Upper Horizontal Attack Check
            if(LP.X >= 48 && LP.X <= 207 && LP.Y >= 96 && LP.Y <= 127 && Corner.StartsWith("Upper"))
            {
                CurrentlyAttacking = "Upper";
                self.State = Monster.MonsterState.Attacking;
                AttackState();
            }

            // Lower Horizontal Attack Check
            if (LP.X >= 48 && LP.X <= 207 && LP.Y >= 176 && LP.Y <= 207 && Corner.StartsWith("Lower"))
            {
                CurrentlyAttacking = "Lower";
                self.State = Monster.MonsterState.Attacking;
                AttackState();
            }

            // Right Veritcal Attack Check
            if (LP.X >= 192 && LP.X <= 223 && LP.Y >= 128 && LP.Y <= 175 && Corner.EndsWith("Right"))
            {
                CurrentlyAttacking = "Right";
                self.State = Monster.MonsterState.Attacking;
                AttackState();
            }

            // Left Veritcal Attack Check
            if (LP.X >= 32 && LP.X <= 63 && LP.Y >= 128 && LP.Y <= 175 && Corner.EndsWith("Left"))
            {
                self.State = Monster.MonsterState.Attacking;
                CurrentlyAttacking = "Left";
                AttackState();
            }
        }

        private string DetermineCorner()
        { 
            string corner = "";

            if (StartingPosition.X == 32 && StartingPosition.Y == 96)
            {
                corner = "UpperLeft";
            }
            else if (StartingPosition.X == 208 && StartingPosition.Y == 96)
            {
                corner = "UpperRight";
            }
            else if (StartingPosition.X == 208 && StartingPosition.Y == 192)
            {
                corner = "LowerRight";
            }
            else if (StartingPosition.X == 32 && StartingPosition.Y == 192)
            {
                corner = "LowerLeft";
            }
            return corner;
        }

    }
}
