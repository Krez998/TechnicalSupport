namespace TechnicalSupport.Models.TicketModels;

public record GetAllTicketsRequest (TicketStatus? Status, bool IsShowNotAssigned);

