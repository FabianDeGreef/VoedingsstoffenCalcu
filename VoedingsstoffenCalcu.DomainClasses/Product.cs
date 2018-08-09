using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VoedingsstoffenCalcu.DomainClasses
{
    public class Product : INotifyPropertyChanged
    {
        private string _naam;
        private decimal _kilocalorieën;
        private decimal _kilojoules;
        private decimal _water;
        private decimal _eiwit;
        private decimal _koolhydraten;
        private decimal _suikers;
        private decimal _vet;
        private decimal _verzadigdVet;
        private decimal _enkelvoudigOnverzadigdVet;
        private decimal _meervoudigOnverzadigdeVetzuren;
        private decimal _cholesterol;
        private decimal _vezels;

        public int ProductId { get; set; }

        public string Naam
        {
            get => _naam;
            set
            {
                _naam = value;
                OnPropertyChanged("Naam");
            }
        }

        public decimal Kilocalorieën
        {
            get => _kilocalorieën;
            set
            {
                _kilocalorieën = value;
                OnPropertyChanged("Kilocalorieën");
            }
        }

        public decimal Kilojoules
        {
            get => _kilojoules;
            set
            {
                _kilojoules = value;
                OnPropertyChanged("Kilojoules");
            }
        }


        public decimal Water
        {
            get => _water;
            set
            {
                _water = value;
                OnPropertyChanged("Water");
            }
        }

        public decimal Eiwit
        {
            get => _eiwit;
            set
            {
                _eiwit = value;
                OnPropertyChanged("Eiwit");
            }
        }

        public decimal Koolhydraten
        {
            get => _koolhydraten;
            set
            {
                _koolhydraten = value;
                OnPropertyChanged("Koolhydraten");
            }
        }

        public decimal Suikers
        {
            get => _suikers;
            set
            {
                _suikers = value;
                OnPropertyChanged("Suikers");
            }
        }

        public decimal Vet
        {
            get => _vet;
            set
            {
                _vet = value;
                OnPropertyChanged("Vet");
            }
        }


        public decimal VerzadigdVet
        {
            get => _verzadigdVet;
            set
            {
                _verzadigdVet = value;
                OnPropertyChanged("VerzadigdVet");
            }
        }

        public decimal EnkelvoudigOnverzadigdVet
        {
            get => _enkelvoudigOnverzadigdVet;
            set
            {
                _enkelvoudigOnverzadigdVet = value;
                OnPropertyChanged("EnkelvoudigOnverzadigdVet");
            }
        }

        public decimal MeervoudigOnverzadigdeVetzuren
        {
            get => _meervoudigOnverzadigdeVetzuren;
            set
            {
                _meervoudigOnverzadigdeVetzuren = value;
                OnPropertyChanged("MeervoudigOnverzadigdeVetzuren");
            }
        }

        public decimal Cholesterol
        {
            get => _cholesterol;
            set
            {
                _cholesterol = value;
                OnPropertyChanged("Cholesterol");
            }
        }

        public decimal Vezels
        {
            get => _vezels;
            set
            {
                _vezels = value;
                OnPropertyChanged("Vezels");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

    }
}
