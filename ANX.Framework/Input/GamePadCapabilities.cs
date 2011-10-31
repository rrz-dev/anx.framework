#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Input
{
    public struct GamePadCapabilities
    {
        private GamePadType gamePadType;
        private bool hasAButton;
        private bool hasBackButton;
        private bool hasBButton;
        private bool hasBigButton;
        private bool hasDPadDownButton;
        private bool hasDPadLeftButton;
        private bool hasDPadRightButton;
        private bool hasDPadUpButton;
        private bool hasLeftShoulderButton;
        private bool hasLeftStickButton;
        private bool hasLeftTrigger;
        private bool hasLeftVibrationMotor;
        private bool hasLeftXThumbStick;
        private bool hasLeftYThumbStick;
        private bool hasRightShoulderButton;
        private bool hasRightStickButton;
        private bool hasRightVibrationMotor;
        private bool hasRightTrigger;
        private bool hasRightXThumbStick;
        private bool hasRightYThumbStick;
        private bool hasStartButton;
        private bool hasVoiceSupport;
        private bool hasXButton;
        private bool hasYButton;
        private bool isConnected;

        //public GamePadCapabilities(GamePadType gamePadType, bool hasAButton, bool hasBackButton, bool hasBButton, bool hasBigButton, bool hasDPadDownButton, bool hasDPadLeftButton, bool hasDPadRightButton, bool hasDPadUpButton, bool hasLeftShoulderButton, bool hasLeftStickButton, bool hasLeftTrigger, bool hasLeftVibrationMotor, bool hasLeftXThumbStick, bool hasLeftYThumbStick, bool hasRightShoulderButton, bool hasRightStickButton, bool hasRightVibrationMotor, bool hasRightTrigger, bool hasRightXThumbStick, bool hasRightYThumbStick, bool hasStartButton, bool hasVoiceSupport, bool hasXButton, bool hasYButton, bool isConnected)
        //{
        //    this.gamePadType = gamePadType;                                                                                                                                                                                                                                                                                                                                                                                                 
        //    this.hasAButton = hasAButton;                                                                                                                                                                                                                                                                                                                                                                                                   
        //    this.hasBackButton = hasBackButton;                                                                                                                                                                                                                                                                                                                                                                                             
        //    this.hasBButton = hasBButton;                                                                                                                                                                                                                                                                                                                                                                                                   
        //    this.hasBigButton = hasBigButton;
        //    this.hasDPadDownButton = hasDPadDownButton;                                                                                                                                                                                                                                                                                                                                                                                                        
        //    this.hasDPadLeftButton= hasDPadLeftButton;
        //    this.hasDPadRightButton = hasDPadRightButton;
        //    this.hasDPadUpButton = hasDPadUpButton;                                                                                                                                                                                                                        
        //    this.hasLeftShoulderButton=hasLeftShoulderButton;                                                                                                                                                                                                                  
        //    this.hasLeftStickButton=hasLeftStickButton;                                                                                                                                                                                                                     
        //    this.hasLeftTrigger=hasLeftTrigger;                                                                                                                                                                                                                         
        //    this.hasLeftVibrationMotor=hasLeftVibrationMotor;                                                                                                                                                                                                                  
        //    this.hasLeftXThumbStick=hasLeftXThumbStick;                                                                                               
        //    this.hasLeftYThumbStick=hasLeftYThumbStick;                                                                                               
        //    this.hasRightShoulderButton=hasRightShoulderButton;                                                                                           
        //    this.hasRightStickButton=hasRightStickButton;                                                                                              
        //    this.hasRightVibrationMotor=hasRightVibrationMotor;                                                                                           
        //    this.hasRightTrigger=hasRightTrigger;
        //    this.hasRightXThumbStick=hasRightXThumbStick;
        //    this.hasRightYThumbStick=hasRightYThumbStick;
        //    this.hasStartButton=hasStartButton;
        //    this.hasVoiceSupport=hasVoiceSupport;
        //    this.hasXButton=hasXButton;
        //    this.hasYButton=hasYButton;
        //    this.isConnected=isConnected;

        //}
        //public GamePadType GamePadType { get { return this.gamePadType; } }
        //public bool HasAButton { get { return this.hasAButton; } }
        //public bool HasBackButton { get { return this.hasBackButton; } }
        //public bool HasBButton { get { return this.hasBButton; } }
        //public bool HasBigButton {  get; private set; }
        //public bool HasDPadDownButton {  get; private set; }
        //public bool HasDPadLeftButton {  get; private set; }
        //public bool HasDPadRightButton {  get; private set; }
        //public bool HasDPadUpButton {  get; private set; }
        //public bool HasLeftShoulderButton {  get; private set; }
        //public bool HasLeftStickButton {  get; private set; }
        //public bool HasLeftTrigger {  get; private set; }
        //public bool HasLeftVibrationMotor {  get; private set; }
        //public bool HasLeftXThumbStick {  get; private set; }
        //public bool HasLeftYThumbStick {  get; private set; }
        //public bool HasRightShoulderButton {  get; private set; }
        //public bool HasRightStickButton {  get; private set; }
        //public bool HasRightVibrationMotor {  get; private set; }
        //public bool HasRightTrigger {  get; private set; }
        //public bool HasRightXThumbStick {  get; private set; }
        //public bool HasRightYThumbStick {  get; private set; }
        //public bool HasStartButton {  get; private set; }
        //public bool HasVoiceSupport {  get; private set; }
        //public bool HasXButton {  get; private set; }
        //public bool HasYButton {  get; private set; }
        //public bool IsConnected {  get; private set; }
    }
}
