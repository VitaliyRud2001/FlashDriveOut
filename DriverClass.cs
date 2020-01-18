using System;
using System.IO;

namespace Test
{
    public class DriverClass
    {
        static public bool IfElemPresentInDriver(DriveInfo drive, DriveInfo[] drivesOnStart)
        {
            foreach (var driveOnStart in drivesOnStart)
            {
                if (drive.Name == driveOnStart.Name)
                {
                    return true;
                }
            }
            return false;
        }

        static public DriveInfo GetLastAddedDriver(DriveInfo[] drivesAfterInput, DriveInfo[] drivesOnStart)
        {
            foreach (var driveAfterInput in drivesAfterInput)
            {
                if (IfElemPresentInDriver(driveAfterInput, drivesOnStart) == false)
                {
                    return driveAfterInput;
                }

            }
            return null;
	    Console.WriteLine("Ti pidor");
        }
    }
}
