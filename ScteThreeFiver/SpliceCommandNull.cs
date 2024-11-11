namespace ScteThreeFiver
{
    public class SpliceCommandNull : SpliceCommand
    {
        // there really is nothing to see here!

        // Spec states:
        //   The splice_null() command is provided for extensibility of the standard.The splice_null()
        //   command allows a splice_info_table to be sent that can carry descriptors without having to send
        //   one of the other defined commands. This command may also be used as a “heartbeat message”
        //   for monitoring cue injection equipment integrity and link integrity.
    }

}