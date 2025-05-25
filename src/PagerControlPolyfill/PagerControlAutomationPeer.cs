﻿using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using System;

namespace PagerControlPolyfill
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1024:Use properties where appropriate", Justification = "This is part of the WinUI API, must not change.")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "This is part of the WinUI API, must not change.")]
	public class PagerControlAutomationPeer : FrameworkElementAutomationPeer
	{
		public PagerControlAutomationPeer(PagerControl owner) : base(owner)
		{
		}

		// IAutomationPeerOverrides
		public new object GetPatternCore(PatternInterface patternInterface)
		{
			if (patternInterface == PatternInterface.Selection)
			{
				return this;
			}

			return base.GetPatternCore(patternInterface);
		}

		public new string GetClassNameCore()
		{
			return nameof(PagerControl);
		}

		public new string GetNameCore()
		{
			var name = base.GetNameCore();

			if (string.IsNullOrEmpty(name))
			{
				if (Owner is PagerControl pagerControl)

				{
					name = pagerControl.GetValue(AutomationProperties.NameProperty).ToString();
				}
			}

			return name!;
		}

		public new AutomationControlType GetAutomationControlTypeCore()
		{
			return AutomationControlType.Menu;
		}

		PagerControl? GetImpl()
		{
			PagerControl? impl = null;

			if (Owner is PagerControl pagerControl)
			{
				impl = pagerControl;
			}

			return impl;
		}

		public object[] GetSelection()
		{
			if (GetImpl() is PagerControl pagerControl)
			{
				return new object[] { pagerControl.SelectedPageIndex };
			}
			return Array.Empty<object>();
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1030:Use events where appropriate", Justification = "This is part of the original WinUI implementation, must not change.")]
		public void RaiseSelectionChanged(double oldIndex, double newIndex)
		{
			if (ListenerExists(AutomationEvents.PropertyChanged))
			{
				RaisePropertyChangedEvent(SelectionPatternIdentifiers.SelectionProperty,
					oldIndex,
					newIndex);
			}
		}
	}
}