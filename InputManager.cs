using UnityEngine;
using System.Collections;

public static class InputManager  {

	
   public static bool Moga = false;
     public static float GetAxis(string inputName, OuyaSDK.OuyaPlayer player ) 
    {
        switch (Moga)
        {
	       case true:
		         switch (inputName)
		        {
		            case "TurnRight":
		                return MogaInput.GetAxis("R2");//MogaController.KEYCODE_BUTTON_R2;//R2
		            case "TurnLeft":
		                return MogaInput.GetAxis("L2");//MogaController.KEYCODE_BUTTON_L2;//L2
		            case "Roll":
		                return MogaInput.GetAxis("Horizontal");//MogaController.AXIS_X;//X
		            case "Vertical":
		                return MogaInput.GetAxis("Vertical");//MogaController.AXIS_Y;//Y
		            case "Pitch":
		                return MogaInput.GetAxis("LookVertical");//MogaController.AXIS_RZ;//RZ
		            case "Yaw":
		                return MogaInput.GetAxis("LookHorizontal");//MogaController.AXIS_Z;//Z
		           default:
				return 0;
		        }
	        
	        case false:
	        	
	        	switch (inputName)
		        {
		            case "TurnRight":
		                return OuyaExampleCommon.GetAxis("RT", player);
		            case "TurnLeft":
		                return OuyaExampleCommon.GetAxis("LT", player);
		            case "Roll":
		                return OuyaExampleCommon.GetAxis("LX", player);
		            case "Vertical":
		                return OuyaExampleCommon.GetAxis("LY", player);
		            case "Pitch":
		                return OuyaExampleCommon.GetAxis("RY", player);
		            case "Yaw":
		                return OuyaExampleCommon.GetAxis("RX", player);
		           default:
				return 0;
		        }
	     }  
		return 0;
    }

    public static bool GetButton(string inputName,OuyaSDK.OuyaPlayer player) 
    {
        
         switch (Moga)
        {
	       case true:
        
			        switch (inputName)
			        {
			            case "Shoot":
			           	 	return MogaInput.GetKey(KeyCode.JoystickButton0);//a
			            case "Orbit":
			                return MogaInput.GetKey(KeyCode.JoystickButton2);//x
			            case "Option":
			               return MogaInput.GetKey(KeyCode.JoystickButton3);    //y 
			            case "Exit":
			                return MogaInput.GetKey(KeyCode.JoystickButton1);       //b
			            case "TurnLeft":
			                return MogaInput.GetKeyDown(KeyCode.JoystickButton10);
			            case "TurnRight":
			               return MogaInput.GetKeyDown(KeyCode.JoystickButton11);
			               
			            case "SkipLeft":
			               return MogaInput.GetKeyDown(KeyCode.JoystickButton4);
			            case "SkipRight":
			               return MogaInput.GetKeyDown(KeyCode.JoystickButton5);
			               
			            case "DLeft":
			            	return MogaInput.GetKey(KeyCode.JoystickButton14); 
			            case "DRight":
			            	return MogaInput.GetKey(KeyCode.JoystickButton15);
			            case "DUp":
			            	return MogaInput.GetKey(KeyCode.JoystickButton12);
			            case "DDown":
			            	return MogaInput.GetKey(KeyCode.JoystickButton13);	
			            case "StartBtn":
			            	return MogaInput.GetKeyDown(KeyCode.JoystickButton7);
				    default:
					return false;
			        }
			case false:      
			
			  	 switch (inputName)
			        {
			            case "Shoot":
			                return OuyaExampleCommon.GetButton( OuyaSDK.KeyEnum.BUTTON_O, player);
			            case "Orbit":
			                return OuyaExampleCommon.GetButton(OuyaSDK.KeyEnum.BUTTON_U, player);  
			            case "Option":
			                return OuyaExampleCommon.GetButton( OuyaSDK.KeyEnum.BUTTON_Y, player);      
			            case "Exit":
			                return OuyaExampleCommon.GetButton( OuyaSDK.KeyEnum.BUTTON_A, player);       
			            case "TurnLeft":
			                return OuyaExampleCommon.GetButton( OuyaSDK.KeyEnum.BUTTON_LT, player);
			            case "TurnRight":
			                return OuyaExampleCommon.GetButton( OuyaSDK.KeyEnum.BUTTON_RT, player);
			            case "SkipLeft":
			                return OuyaExampleCommon.GetButton( OuyaSDK.KeyEnum.BUTTON_LB, player);
			            case "SkipRight":
			                return OuyaExampleCommon.GetButton( OuyaSDK.KeyEnum.BUTTON_RB, player);
			            case "DLeft":
			            	return OuyaExampleCommon.GetButton( OuyaSDK.KeyEnum.BUTTON_DPAD_LEFT, player  );  
			            case "DRight":
			            	return OuyaExampleCommon.GetButton( OuyaSDK.KeyEnum.BUTTON_DPAD_RIGHT, player  ); 
			            case "DUp":
			            	return OuyaExampleCommon.GetButton( OuyaSDK.KeyEnum.BUTTON_DPAD_UP , player );
			            case "DDown":
			            	return OuyaExampleCommon.GetButton( OuyaSDK.KeyEnum.BUTTON_DPAD_DOWN , player );		 	
				    default:
					return false;
			        }  
    }
		return false;
    }

}
