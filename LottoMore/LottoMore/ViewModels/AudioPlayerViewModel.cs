using LottoMore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LottoMore.ViewModels
{
    public class AudioPlayerViewModel : BaseViewModel
    {
        private  IAudioPlayerService _audioPlayer;
        private string _commandText;
        private string _playerButtonIcon;

        private bool _isStopped;
        public string PlayButtonIcon
        { 
            get => _playerButtonIcon;
            set
            {
                if (_playerButtonIcon == value)
                    return;
                OnPropertyChanged("PlayButtonIcon");
            }
        }

        public AudioPlayerViewModel( IAudioPlayerService audioPlayer)
        {
            _playerButtonIcon = "play_icon";

            _audioPlayer = audioPlayer;
            _audioPlayer.OnFinishPlaying = () =>
            {
                _isStopped = true;
                CommandText = "Play";

            };

            CommandText = "Play";
            _isStopped = true;
        }


        private ICommand _playPauseCommand;
        public ICommand PlayPauseCommand
        {
            get
            {
                return _playPauseCommand ?? (_playPauseCommand = new Command(
                  (obj) =>
                  {
                      if (CommandText == "Play")
                      {
                          if (_isStopped)
                          {
                              _isStopped = false;
                              _audioPlayer.Play("audio.mp3");
                              _playerButtonIcon = "paused_icon";

                          }
                          else
                          {
                              _audioPlayer.Play();
                          }
                          CommandText = "Pause";
                      }
                      else
                      {
                          _audioPlayer.Pause();
                          CommandText = "Play";
                      }
                  }));
            }
        }

        public string CommandText 
        {
            get => _commandText;
            set 
            {
                _commandText = value;
                OnPropertyChanged();
            }
        }
    }
}
