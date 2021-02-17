﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sprintfinity3902.Sprites {
    public class Animation {
        public bool IsPlaying { get; set; }
        public bool PlayOneTime { get; private set; }
        public float PlaybackProgress { get; private set; }

        private List<AnimationFrame> _frames = new List<AnimationFrame>();


        public Animation() {
            IsPlaying = true;
        }

        public Animation(bool shouldPlay) {
            IsPlaying = shouldPlay;
        }

        public AnimationFrame GetFrame(int index) {
            if (index < 0 || index >= _frames.Count) {
                throw new ArgumentOutOfRangeException();
            }

            return _frames[index];
        }

        public AnimationFrame CurrentFrame {
            get {
                return _frames
                    .Where(f => f.TimeStamp <= PlaybackProgress)
                    .OrderBy(f => f.TimeStamp)
                    .LastOrDefault();
            }
        }

        public float Duration {
            get {
                if (!_frames.Any())
                    return 0;

                return _frames.Max(f => f.TimeStamp);
            }
        }

        
        public void AddFrame(SpriteFrame sprite, float timeStamp) { 
            _frames.Add(new AnimationFrame(sprite, timeStamp));
        }

        public void Update(GameTime gameTime) {
            
            if (IsPlaying) {
                PlaybackProgress += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (PlaybackProgress > Duration) {
                    if (PlayOneTime) {
                        PlaybackProgress -= Duration;
                        Stop();
                        //PlayOneTime = false;
                    } else {
                        PlaybackProgress -= Duration;
                    }
                }     
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color) {
            spriteBatch.Draw(CurrentFrame.Sprite.Texture, position, CurrentFrame.Sprite.SourceRectangle, color, 0.0f, new Vector2(0, 0), 5.0f, SpriteEffects.None, 0.0f);
        }

        public void Play() {
            IsPlaying = true;
        }

        public void Stop() {
            IsPlaying = false;
            PlaybackProgress = 0;
        }

        public void PlayOnce() {
            PlayOneTime = true;
            Play();
        }

    }
}
