using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoMore.Interfaces
{
    public interface IAudioPlayerService
    {
        Action OnFinishPlaying { get; set; }
        void Play(string pathToAudioFIle);
        void Play();
        void Pause();

    }
}
