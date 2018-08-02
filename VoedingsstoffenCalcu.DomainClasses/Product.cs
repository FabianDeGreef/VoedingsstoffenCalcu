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
            get { return _naam ;}
            set
            {
                _naam = value;
                OnPropertyChanged("Naam");
            }
        }

        public decimal Kilocalorieën
        {
            get { return _kilocalorieën; }
            set
            {
                _kilocalorieën = value;
                OnPropertyChanged("Kilocalorieën");
            }
        }

        public decimal Kilojoules
        {
            get { return _kilojoules; }
            set
            {
                _kilojoules = value;
                OnPropertyChanged("Kilojoules");
            }
        }


        public decimal Water
        {
            get { return _water; }
            set
            {
                _water = value;
                OnPropertyChanged("Water");
            }
        }

        public decimal Eiwit
        {
            get { return _eiwit; }
            set
            {
                _eiwit = value;
                OnPropertyChanged("Eiwit");
            }
        }

        public decimal Koolhydraten
        {
            get { return _koolhydraten; }
            set
            {
                _koolhydraten = value;
                OnPropertyChanged("Koolhydraten");
            }
        }

        public decimal Suikers
        {
            get { return _suikers; }
            set
            {
                _suikers = value;
                OnPropertyChanged("Suikers");
            }
        }

        public decimal Vet
        {
            get { return _vet; }
            set
            {
                _vet = value;
                OnPropertyChanged("Vet");
            }
        }


        public decimal VerzadigdVet
        {
            get { return _verzadigdVet; }
            set
            {
                _verzadigdVet = value;
                OnPropertyChanged("VerzadigdVet");
            }
        }

        public decimal EnkelvoudigOnverzadigdVet
        {
            get { return _enkelvoudigOnverzadigdVet; }
            set
            {
                _enkelvoudigOnverzadigdVet = value;
                OnPropertyChanged("EnkelvoudigOnverzadigdVet");
            }
        }

        public decimal MeervoudigOnverzadigdeVetzuren
        {
            get { return _meervoudigOnverzadigdeVetzuren; }
            set
            {
                _meervoudigOnverzadigdeVetzuren = value;
                OnPropertyChanged("MeervoudigOnverzadigdeVetzuren");
            }
        }

        public decimal Cholesterol
        {
            get { return _cholesterol; }
            set
            {
                _cholesterol = value;
                OnPropertyChanged("Cholesterol");
            }
        }

        public decimal Vezels
        {
            get { return _vezels; }
            set
            {
                _vezels = value;
                OnPropertyChanged("Vezels");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

    }
}
