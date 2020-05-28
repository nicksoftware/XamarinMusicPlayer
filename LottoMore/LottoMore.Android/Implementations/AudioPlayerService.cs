using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LottoMore.Droid.Implementations;
using LottoMore.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioPlayerService))]

namespace LottoMore.Droid.Implementations
{
    class AudioPlayerService : IAudioPlayerService
    {

        private MediaPlayer _mediaPlayer;

        public AudioPlayerService()
        {

        }
        public Action OnFinishPlaying { get; set; }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Play(string pathToAudioFIle)
        {
            // Check if _audioPlayer is currently playing  
            if (_mediaPlayer != null)
            {
                _mediaPlayer.Completion -= MediaPlayer_Completion;
                _mediaPlayer.Stop();
            }

            var fullPath = pathToAudioFIle;

            Android.Content.Res.AssetFileDescriptor afd = null;

            try
            {
                afd = MainActivity.CONTEXT.Assets.OpenFd(fullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error openfd: " + ex);
            }
            if (afd != null)
            {
                System.Diagnostics.Debug.WriteLine("Length " + afd.Length);
                if (_mediaPlayer == null)
                {
                    _mediaPlayer = new MediaPlayer();
                    _mediaPlayer.Prepared += (sender, args) =>
                    {
                        _mediaPlayer.Start();
                        _mediaPlayer.Completion += MediaPlayer_Completion;
                    };
                }

                _mediaPlayer.Reset();
                _mediaPlayer.SetVolume(1.0f, 1.0f);

                _mediaPlayer.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
                _mediaPlayer.Prepare();
                _mediaPlayer.Start();
            }
        }

        private void MediaPlayer_Completion(object sender, EventArgs e)
        {
            OnFinishPlaying?.Invoke();
        }

        public void Play()
        {
            OnFinishPlaying?.Invoke();
        }
    }
}