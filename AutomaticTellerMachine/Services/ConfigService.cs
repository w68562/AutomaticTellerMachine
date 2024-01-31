using AutomaticTellerMachine.DataTransportLayer;
using AutomaticTellerMachine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomaticTellerMachine.Services
{
    public class ConfigService
    {
        private readonly FileService fileService;
        private Config Config { get; set; }
        public ConfigService(FileService fileService) { 
            this.fileService = fileService;
            InitConfig();
        }

        public void InitConfig()
        {
            Config = new Config() { AcceptedProviders = fileService.GetConfig()};
        }

        public bool UpdateConfig(string provider) { 
            if(!IsProviderAccepted(provider))
            {
                Config.AcceptedProviders.Add(provider);
                fileService.SaveConfig(Config.AcceptedProviders);
                return true;
            }
            return false;
        }

        public void DeleteProvider(string provider)
        {
            if (IsProviderAccepted(provider)) {
                Config.AcceptedProviders.Remove(provider);
                fileService.SaveConfig(Config.AcceptedProviders);
            }
        }

        public bool IsProviderAccepted(string provider)
        {
            return Config.AcceptedProviders.Contains(provider);
        }

        public List<string> GetProviders()
        {
            return Config.AcceptedProviders;
        }
    }
}
