using CoreSynth.Generators;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoreSynth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }

        public Oscillator Osc1 = new Oscillator(44100);

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // https://stackoverflow.com/questions/7008561/naudio-algorithm-to-play-a-sinewave-whose-frequency-can-be-changed-smoothly-in-r

            Osc1.Frequency = Convert.ToSingle(FrequencyInputTextBox1.Text);

            Osc1.GenerateSine();

            Play(Osc1.GetByteWave());
        }

        private void Play(byte[] buffer)
        {
            var source = new LoopStream(
                new RawSourceWaveStream(
                    new MemoryStream(buffer),
                    new WaveFormat()
                ));

            we.Init(source);
            we.Play();
        }

        private WaveOutEvent we = new WaveOutEvent();

        private bool _isPlaying = false;

        private void OSC1PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isPlaying)
            {
                we.Stop();
                _isPlaying = !_isPlaying;

                return;
            }

            _isPlaying = !_isPlaying;

            Osc1.Frequency = Convert.ToSingle(OSC1FrequencySlider.Value);
            Osc1.Amplitude = Convert.ToSingle(OSC1AmplitudeSlider.Value);
            Osc1.Phase = Convert.ToSingle(OSC1PhaseSlider.Value);
            Osc1.SampleRate = Convert.ToSingle(OSC1SampleRateTextBox.Text);

            Osc1.GenerateSine();

            Play(Osc1.GetByteWave());
        }

        private void OSC1FrequencySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_isPlaying)
                return;

            we.Stop();

            Osc1.Frequency = Convert.ToSingle(OSC1FrequencySlider.Value);

            Osc1.GenerateSine();

            Play(Osc1.GetByteWave());
        }
    }
}
