namespace pimp.ECS.Components
{
    public struct ConfigComponent
    {
        public string Name;
        public int TranscoderThreads;
        public int TranscoderQueueSize;
        public int UploaderThreads;
        public int UploaderQueueSize;
        public int PrefetcherThreads;
        public int PrefetcherQueueSize;
    }
}