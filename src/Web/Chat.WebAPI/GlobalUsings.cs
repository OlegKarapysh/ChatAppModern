﻿global using System.Text.Json;
global using System.Text;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.SignalR;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Mvc;
global using Chat.Domain.DTOs.Messages;
global using Chat.Application.SignalR;
global using Chat.Domain.DTOs.Calls;
global using Chat.Application.Services.Connections;
global using Chat.Application.RequestExceptions;
global using Chat.Application.JWT;
global using Chat.Application.Services.Authentication;
global using Chat.Application.Services.JWT;
global using Chat.Application.Services.Messages;
global using Chat.Application.Services.Users;
global using Chat.DomainServices.UnitsOfWork;
global using Chat.Persistence.UnitsOfWork;
global using Chat.Domain.DTOs.Authentication;
global using Chat.Application.Extensions;
global using Chat.Domain.DTOs.Conversations;
global using Chat.Domain.DTOs.Users;
global using Chat.Persistence.Contexts;
global using Chat.Domain.Entities;
global using Chat.WebAPI.Extensions;
global using Chat.WebAPI.Middlewares;
global using Chat.WebAPI.SignalR;
