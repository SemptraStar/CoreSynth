using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSynth.Generators
{
    public class Oscillator
    {
        public float Amplitude { get; set; } = 50;

        public float SampleRate { get; set; } = 44100;

        private float _frequency;
        public float Frequency
        {
            get
            {
                return _frequency;
            }
            set
            {
                _frequency = value;

                AngularFrequency = 2 * (float)Math.PI * _frequency;
            }
        }

        public float AngularFrequency { get; private set; }

        public float Phase { get; set; } = 0;

        public sbyte[] Buffer { get; set; }

        public Oscillator(int size)
        {
            Buffer = new sbyte[size];
        }

        public sbyte[] GenerateSine()
        {
            for (int i = 0; i < Buffer.Length; i++)
            {
                Buffer[i] = Convert.ToSByte(Amplitude * Math.Sin(AngularFrequency * i / SampleRate + Phase));
            }

            return Buffer;
        }

        public Oscillator AddWave(Oscillator other)
        {
            for (int i = 0; i < Buffer.Length; i++)
            {
                sbyte b1 = Buffer[i];
                sbyte b2 = other.Buffer[i];
                Buffer[i] = Convert.ToSByte((Buffer[i] / 2) + (other.Buffer[i] / 2));
            }

            return this;
        }

        public byte[] GetByteWave()
        {
            return Buffer.Select(x => Convert.ToByte(Math.Abs(x))).ToArray();
        }
    }
}
