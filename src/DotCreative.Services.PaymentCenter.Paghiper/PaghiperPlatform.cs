using DotCreative.Services.PaymentCenter.Core.Abstractions;
using DotCreative.Services.PaymentCenter.Core.Entities;
using DotCreative.Services.PaymentCenter.Core.Shared.Enums;

namespace DotCreative.Services.PaymentCenter.Paghiper;

public partial class PaghiperPlatform : Platform
{
  public PaghiperPlatform(PlatformData platformData) : base(platformData, EPlatforms.PagHiper)
  {
  }
}