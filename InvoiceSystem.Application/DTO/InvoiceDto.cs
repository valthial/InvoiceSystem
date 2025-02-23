﻿using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Dto;

public class InvoiceDTO
{
    public string Id { get; set; }
    public DateTimeOffset DateIssued { get; set; }
    public decimal NetAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Description { get; set; }
    public Guid CounterPartyCompanyId { get; set; }
    public Company? CounterPartyCompany { get; set; }
}