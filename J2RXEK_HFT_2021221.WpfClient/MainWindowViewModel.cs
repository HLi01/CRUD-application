using J2RXEK_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace J2RXEK_HFT_2021221.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private Driver selectedDriver;
        public Driver SelectedDriver
        {
            get { return selectedDriver; }
            set 
            {
                if (value!=null)
                {
                    selectedDriver = new Driver()
                    {
                        Name = value.Name,
                        Id = value.Id,
                        Age=value.Age,
                        Number = value.Number,
                        DebutYear=value.DebutYear,
                        TeamId=value.TeamId
                    };
                }
                SetProperty(ref selectedDriver, value);
                (DeleteDriver as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        private Team selectedTeam;
        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (value != null)
                {
                    selectedTeam = new Team()
                    {
                        TeamName = value.TeamName,
                        Id = value.Id,
                        TeamPrincipal=value.TeamPrincipal,
                        ChampionshipsWon=value.ChampionshipsWon,
                        PowerUnit=value.PowerUnit
                    };
                }
                SetProperty(ref selectedTeam, value);
                (DeleteTeam as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        private Championship selectedChampionship;
        public Championship SelectedChampionship
        {
            get { return selectedChampionship; }
            set
            {
                if (value != null)
                {
                    selectedChampionship = new Championship()
                    {
                        Year = value.Year,
                        Id = value.Id,
                        NumberOfRaces=value.NumberOfRaces,
                        WCC=value.WCC
                    };
                }
                SetProperty(ref selectedChampionship, value);
                (DeleteChampionship as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public RestCollection<Driver> Drivers {get;set;}
        public RestCollection<Team> Teams {get;set;}
        public RestCollection<Championship> Championships {get;set;}
        public ICommand CreateDriver { get; set; }
        public ICommand DeleteDriver { get; set; }
        public ICommand UpdateDriver { get; set; }
        public ICommand CreateTeam { get; set; }
        public ICommand DeleteTeam { get; set; }
        public ICommand UpdateTeam { get; set; }
        public ICommand CreateChampionship { get; set; }
        public ICommand DeleteChampionship { get; set; }
        public ICommand UpdateChampionship { get; set; }
        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Drivers = new RestCollection<Driver>("http://localhost:65297/", "driver", "hub");
                Teams = new RestCollection<Team>("http://localhost:65297/", "team", "hub");
                Championships = new RestCollection<Championship>("http://localhost:65297/", "championship", "hub");

                CreateDriver = new RelayCommand(() =>
                  {
                      Drivers.Add(new Driver()
                      {
                          Name = SelectedDriver.Name,
                          Number= SelectedDriver.Number,
                          Age=SelectedDriver.Age,
                          DebutYear=SelectedDriver.DebutYear,

                      });
                  });
                UpdateDriver = new RelayCommand(() =>
                {
                    Drivers.Update(SelectedDriver);
                });
                DeleteDriver = new RelayCommand(() =>
                {
                    Drivers.Delete(SelectedDriver.Id);
                }, () =>
                {
                    return SelectedDriver != null;
                });
                SelectedDriver = new Driver();

                CreateTeam = new RelayCommand(() =>
                {
                    Teams.Add(new Team()
                    {
                        TeamName = SelectedTeam.TeamName,
                        ChampionshipsWon=SelectedTeam.ChampionshipsWon,
                        PowerUnit=SelectedTeam.PowerUnit,
                        TeamPrincipal=SelectedTeam.TeamPrincipal
                    });
                });
                UpdateTeam = new RelayCommand(() =>
                {
                    Teams.Update(SelectedTeam);
                });
                DeleteTeam = new RelayCommand(() =>
                {
                    Teams.Delete(selectedTeam.Id);
                }, () =>
                {
                    return SelectedTeam != null;
                });
                SelectedTeam = new Team();

                CreateChampionship = new RelayCommand(() =>
                {
                    Championships.Add(new Championship()
                    {
                        Year = SelectedChampionship.Year,
                        NumberOfRaces= SelectedChampionship.NumberOfRaces,
                        WCC=SelectedChampionship.WCC
                    });
                });
                UpdateChampionship = new RelayCommand(() =>
                {
                    Championships.Update(SelectedChampionship);
                });
                DeleteChampionship = new RelayCommand(() =>
                {
                    Championships.Delete(SelectedChampionship.Id);
                }, () =>
                {
                    return SelectedChampionship != null;
                });
                SelectedChampionship = new Championship();
            }
        }
    }
}
