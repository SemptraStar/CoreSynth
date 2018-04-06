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

        public Oscillator Osc1 = new Oscillator(100000);

        public Oscillator Osc2 = new Oscillator(100000);

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Osc1.Frequency = Convert.ToSingle(FrequencyInputTextBox1.Text);
            Osc2.Frequency = Convert.ToSingle(FrequencyInputTextBox2.Text);

            Osc2.Phase = (float)Math.PI / 3;

            Osc1.GenerateSine();
            Osc2.GenerateSine();

            Osc1.AddWave(Osc2);

            Play(Osc1.GetByteWave());
        }

        private void Play(byte[] buffer)
        {
            var source = new RawSourceWaveStream(
                new MemoryStream(buffer),
                new WaveFormat()
                );

            var wv = new WaveOutEvent();
            wv.Init(source);
            wv.Play();
        }
    }
}
