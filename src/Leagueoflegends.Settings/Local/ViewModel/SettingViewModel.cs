﻿using System;
using System.Windows.Input;
using System.Collections.Generic;
using Leagueoflegends.Data.Setting;
using Leagueoflegends.Foundation.Mvvm;
using Leagueoflegends.Foundation.Riotbase;
using Leagueoflegends.Foundation.Riotcore;
using Leagueoflegends.ExampleData.Setting;
using Leagueoflegends.LayoutSupport.Controls;
using Leagueoflegends.Settings.UI.Views;
using Leagueoflegends.Settings.Client.UI.Views;
using Leagueoflegends.Settings.Client.Local.ViewModels;
using Leagueoflegends.Game.Shortcut.Local.ViewModel;
using Leagueoflegends.Game.Shortcut.UI.Views;
using Leagueoflegends.Game.Sound.Local.ViewModel;
using Leagueoflegends.Game.Sound.Views;

namespace Leagueoflegends.Settings.Local.ViewModel
{
	public class SettingViewModel : ObservableObject
	{
		#region Variables

		private Action<IRiotUI> ViewClosed;
		private IRiotUI _currentView;
		private List<SettingMenuModel> _settingMenus;
		private SettingMenuModel _currentSettingMenu;

		private AlarmViewModel Alarm;
		private ChatViewModel Chat;
		private NormalViewModel Normal;
		private SoundViewModel Sound;
		private VoiceViewModel Voice;
		private GameHotKeyViewModel HotKey;
		private GameSoundViewModel GameSound;
		private Dictionary<int, IRiotUI> UIs { get; set; }
		#endregion 

		#region Command

		public ICommand CompleteCommand { get; set; }
		#endregion

		#region Menus

		public List<SettingMenuModel> SettingMenus
		{
			get => _settingMenus;
			set { _settingMenus = value; OnPropertyChanged(); }
		}

		public SettingMenuModel CurrentSettingMenu
		{
			get => _currentSettingMenu;
			set { _currentSettingMenu = value; OnPropertyChanged(); SettingMenuChanged(value); }
		}
		#endregion

		#region CurrentView

		public IRiotUI CurrentView
		{
			get => _currentView;
			set { _currentView = value; OnPropertyChanged(); }
		}
		#endregion

		#region Constructor

		public SettingViewModel(Action<IRiotUI> modalClose)
		{
			ViewClosed = modalClose;
			UIs = new();
			Normal = new NormalViewModel();
			Alarm = new AlarmViewModel();
			Chat = new ChatViewModel();
			Sound = new SoundViewModel();
			Voice = new VoiceViewModel();
			HotKey = new GameHotKeyViewModel();
			GameSound = new GameSoundViewModel();

			SettingMenus = ExamSettings.GetSettingList();
			CompleteCommand = new RelayCommand<Modal>(CompleteClick);
		}
		#endregion

		#region SettingMenuChanged

		private void SettingMenuChanged(SettingMenuModel value)
		{
			IRiotUI content;
			int key;

			if (value != null)
			{
				key = value.Seq;
				content = value.Seq switch
				{
					// Client
					1 => new NormalView().SetVM(Normal),
					2 => new AlarmView().SetVM(Alarm),
					3 => new ChatView().SetVM(Chat),
					4 => new SoundView().SetVM(Sound),
					5 => new VoiceView().SetVM(Voice),
					6 => new BlockView().SetVM(new BlockViewModel()),

					// Game
					8 => new GameHotKeyView().SetVM(HotKey),
					9 => new GameSoundView().SetVM(GameSound),
					_ => new EmptyView()
				};

				if (!UIs.ContainsKey(key))
				{
					UIs.Add(key, content);
				}

				CurrentView = UIs[key];
			}
		}
		#endregion

		#region CompleteClick

		private void CompleteClick(Modal obj)
		{
			ViewClosed.Invoke(View);

			SettingModel setting = RiotConfig.Config.Settings;
			setting.ClientNormal = Normal.Model;
			setting.ClientAlarm = Alarm.Model;
			setting.ClientChat = Chat.Model;
			setting.ClientSound = Sound.Model;
			setting.ClientVoice = Voice.Model;
			setting.GameHotKey = HotKey.Model;
			setting.GameSound = GameSound.Model;

			RiotConfig.SaveSettings(setting);
		}
		#endregion
	}
}
