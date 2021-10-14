﻿using Lol.Foundation.Mvvm;
using Lol.GameRoom.Local.Models;
using Lol.YamlDatabase.Collection;
using Lol.YamlDatabase.Entites.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lol.GameRoom.Local.ViewModels
{
    public class SummonersRiftViewModel : ObservableObject
    {
        #region Variables

        private List<MyFriends> _friends;
        private List<GuestModel> _guests;
        private Action _riftClose;
        private Action _modeChange;
        #endregion

        #region ICommand

        public RelayCommand<object> ModeChangeCommand { get; set; }
        public RelayCommand<object> CloseCommand { get; set; }
        #endregion

        #region Friends

        public List<MyFriends> Friends
        {
            get => _friends;
            set { _friends = value; OnPropertyChanged(); }
        }
        #endregion

        #region Guests

        public List<GuestModel> Guests
        {
            get => _guests;
            set { _guests = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructor

        public SummonersRiftViewModel(FriendsCollection friends, Action riftClose, Action modeChange)
        {
            CloseCommand = new RelayCommand<object>(RoomCloseCommand, CanRoomCloseCommand);
            ModeChangeCommand = new RelayCommand<object>(ModeChangeCommandExe, CanModeChangeCommand);

            _riftClose = riftClose;
            _modeChange = modeChange;

            Friends = friends.GeneralList.Children.Where(x => x.Status == 3).ToList();

            List<GuestModel> source = new List<GuestModel>();

            source.Add(new GuestModel { Name = "Elena"});
            Guests = source;
        }
        #endregion

        #region RoomCloseCommand

        private void RoomCloseCommand(object obj)
        {
            _riftClose?.Invoke();
        }

        private bool CanRoomCloseCommand(object obj)
        {
            return true;
        }
        #endregion

        #region ModeChangeCommandExe

        #endregion
        private void ModeChangeCommandExe(object obj)
        {
            _modeChange?.Invoke();
        }

        private bool CanModeChangeCommand(object obj)
        {
            return true;
        }
    }
}
