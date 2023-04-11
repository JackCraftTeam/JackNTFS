namespace JackNTFS.src.userinterface.imports
{
    internal class UserInputHandler : AbstractInputHandler
    {
        private string mUserInput;
        private bool mResult;

        public UserInputHandler(RestrictionStyle restr, string expect, Stream importStream, Stream exportStream)
            : base(restr, expect, importStream, exportStream)
        {
            this.mUserInput = "";
            this.mResult = false;
        }

        public override int Handle()
        {
            this.mUserInput = new StreamReader(mImportStream).ReadLine();

            if (mUserInput == null)
            {
                this.mUserInput = "";
                this.mResult = false;
                return -1;
            }

            return base.GetRestrictionStyle() switch
            {
                /* If mUserInput is more than ONE character, then we only compare the first character
                 * in that string regardless to the difference of length. And same to mExpect */
                (RestrictionStyle.SINGULARITY) => ((this.mUserInput[0] == base.GetExpect()[0]) ? 0 : 1),
                (RestrictionStyle.MULTIPARITY) => (String.Compare(this.mUserInput, base.GetExpect())),
                _ => -1,
            };
        }

        public string GetLastInput()
        { return this.mUserInput; }

        public bool GetLastResult()
        { return this.mResult; }
    }
}
