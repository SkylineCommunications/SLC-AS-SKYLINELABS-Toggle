/*
****************************************************************************
*  Copyright (c) 2023,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

14/11/2023	1.0.0.4		TPO, Skyline	Initial version
****************************************************************************
*/

namespace ToggleButton_1
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Text;
	using Skyline.DataMiner.Automation;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			ScriptParam spDmaeid = engine.GetScriptParam("dmaeid");
			ScriptParam spReadParam = engine.GetScriptParam("readParam");
			ScriptParam spEnabledValue = engine.GetScriptParam("enabledValue");
			ScriptParam spDisabledValue = engine.GetScriptParam("disabledValue");

			int readParamID = Convert.ToInt32(spReadParam.Value);
			int enabledValue = Convert.ToInt32(spEnabledValue.Value);
			int disabledValue = Convert.ToInt32(spDisabledValue.Value);

			string[] dmaEidarray = spDmaeid.Value.Split('/');
			int dmaID = Convert.ToInt32(dmaEidarray[0]);
			int eID = Convert.ToInt32(dmaEidarray[1]);

			Element e = engine.FindElement(dmaID, eID);

			int writeParamID = e.GetWriteParameterIDFromRead(readParamID);

			object readParam = e.GetParameter(readParamID);
			int invertedReadValue = Convert.ToInt32(readParam) == enabledValue ? disabledValue : enabledValue;

			e.SetParameter(writeParamID, invertedReadValue);
		}
	}
}