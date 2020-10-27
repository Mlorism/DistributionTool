using DistributionTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro;
using System.Windows;
using System.ComponentModel;

namespace DistributionTool.ViewModels
{
	class SettingsViewModel : BaseViewModel, ITab
	{
		#region Properties
		public static string ThemeColor { get; set; } = "Teal";
		public static string ThemeAccent { get; set; } = "BaseLight";

		private static bool amberTheme { get; set; }
		public static bool AmberTheme 
		{
			get { return amberTheme; }
			set
			{
				amberTheme = value;				
				if(value == true)
				{
					ThemeColor = "Amber";
					ChangeColour();
				}				
			} 
		}

		private static bool limeTheme { get; set; }
		public static bool LimeTheme
		{
			get { return limeTheme; }
			set
			{
				limeTheme = value;				
				if (value == true)
				{
					ThemeColor = "Lime";
					ChangeColour();
				}				
			}
		}

		private static bool steelTheme { get; set; }
		public static bool SteelTheme
		{
			get { return steelTheme; }
			set
			{
				steelTheme = value;				
				if (value == true)
				{
					ThemeColor = "Steel";
					ChangeColour();
				}
				
			}
		}

		private static bool tealTheme { get; set; } = true;
		public static bool TealTheme
		{
			get { return tealTheme; }
			set
			{
				tealTheme = value;				
				if (value == true) 
				{
					ThemeColor = "Teal";
					ChangeColour();				
				} 
			}
		}

		private static bool lightTheme = true;
		public static bool LightTheme
		{
			get { return lightTheme; }
			set 
			{ 
				lightTheme = value; 
				if (value == true)
				{
					ThemeAccent = "BaseLight";
					ChangeColour();
				}
			}
		}

		private static bool darkTheme;
		public static bool DarkTheme
		{
			get { return darkTheme; }
			set 
			{ 
				darkTheme = value;
				if (value == true)
				{
					ThemeAccent = "BaseDark";
					ChangeColour();
				}
			}
		}

		#endregion


		#region Constructor
		public SettingsViewModel()
		{
			TabName = "Settings";			
		}
		#endregion

		#region Methods		
		public static void ChangeColour()
		{
			ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(ThemeColor), ThemeManager.GetAppTheme(ThemeAccent));
		} //ChangeColour() change application color or acccent
		#endregion



	}
}
