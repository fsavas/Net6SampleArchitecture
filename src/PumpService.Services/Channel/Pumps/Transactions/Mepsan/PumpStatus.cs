using PumpService.Core.Defaults;

namespace TarPet.Comm.Pump.Transactions.Mepsan
{
    public class PumpStatus
    {
        public EnumClasses.MepsanStates Status { get; set; }

        public void setStatus(byte pStatusByte)
        {
            switch (pStatusByte)
            {
                case (byte)EnumClasses.MepsanStates.AuthorizedState:
                    Status = EnumClasses.MepsanStates.AuthorizedState;
                    break;

                case (byte)EnumClasses.MepsanStates.FillingCompletedState:
                    Status = EnumClasses.MepsanStates.FillingCompletedState;
                    break;

                case (byte)EnumClasses.MepsanStates.FillingState:
                    Status = EnumClasses.MepsanStates.FillingState;
                    break;

                case (byte)EnumClasses.MepsanStates.MaxAmountVolumeReachedState:
                    Status = EnumClasses.MepsanStates.MaxAmountVolumeReachedState;
                    break;

                case (byte)EnumClasses.MepsanStates.PausedState:
                    Status = EnumClasses.MepsanStates.PausedState;
                    break;

                case (byte)EnumClasses.MepsanStates.ResetState:
                    Status = EnumClasses.MepsanStates.ResetState;
                    break;

                case (byte)EnumClasses.MepsanStates.PumpNotProgrammedState:
                    Status = EnumClasses.MepsanStates.PumpNotProgrammedState;
                    break;

                case (byte)EnumClasses.MepsanStates.SwitchedOffState:
                    Status = EnumClasses.MepsanStates.SwitchedOffState;
                    break;
            }
        }
    }
}