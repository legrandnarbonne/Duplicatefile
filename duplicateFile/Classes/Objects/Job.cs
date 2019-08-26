using System;

namespace duplicateFile.Classes
{
    public class Job
    {
        private Guid id;
        private Config config;

        public Job(Config cfg)
        {
            id = Guid.NewGuid();
            config = (Config)cfg.Clone();
        }

        public Config Config
        {
            get { return config; }
            set { config = value; }
        }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}