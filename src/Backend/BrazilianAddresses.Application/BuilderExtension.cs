﻿using Microsoft.Extensions.DependencyInjection;
using BrazilianAddresses.Application.Services.Cryptography;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule;
using BrazilianAddresses.Application.BusinessRules.UserBusinessRule;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;
using BrazilianAddresses.Application.BusinessRules.UserBusinessRule.Interfaces;

namespace BrazilianAddresses.Application
{
    public static class BuilderExtension
    {
        public static void AddApplication(this IServiceCollection serviceDescriptors)
        {
            AddApplicationIBGE(serviceDescriptors);
            AddApplicationUser(serviceDescriptors);
            AddApplicationServicePasswordEncryption(serviceDescriptors);
        }

        private static void AddApplicationIBGE(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<ICreateIBGE, CreateIBGE>();
            serviceDescriptors.AddScoped<IUpdateIBGE, UpdateIBGE>();
            serviceDescriptors.AddScoped<IGetIBGEAddresses, GetIBGEAddresses>();
            serviceDescriptors.AddScoped<IRemoveIBGE, RemoveIBGE>();
        }

        private static void AddApplicationUser(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<ICreateUser, CreateUser>();
        }

        private static void AddApplicationServicePasswordEncryption(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped(options => new PasswordEncryption());
        }
    }
}