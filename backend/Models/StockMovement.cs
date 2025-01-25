using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StockManagement.Models;

public enum StockMovementStatus
{
    Incoming,   // Stock entrant
    Outgoing,   // Stock sortant
    Transfer    // Transfert de stock
}

public class StockMovement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "ID Mouvement de Stock")]
    public int StockMovementId { get; set; }

    // Relation avec l'emplacement source
    [Required(ErrorMessage = "L'ID de l'emplacement source est obligatoire")]
    [ForeignKey("SourceLocation")]
    [Display(Name = "ID Emplacement Source")]
    public int SourceLocationId { get; set; }

    [ValidateNever]
    [Required(ErrorMessage = "L'emplacement source est obligatoire")]
    [Display(Name = "Emplacement Source")]
    public Location SourceLocation { get; set; }

    // Relation avec l'emplacement de destination
    [Required(ErrorMessage = "L'ID de l'emplacement de destination est obligatoire")]
    [ForeignKey("DestinationLocation")]
    [Display(Name = "ID Emplacement de Destination")]
    public int DestinationLocationId { get; set; }

    [ValidateNever]
    [Required(ErrorMessage = "L'emplacement de destination est obligatoire")]
    [Display(Name = "Emplacement de Destination")]
    public Location DestinationLocation { get; set; }

    [Required(ErrorMessage = "Le type de mouvement est obligatoire")]
    [Display(Name = "Type de Mouvement")]
    public StockMovementStatus MovementType { get; set; }

    [Required(ErrorMessage = "L'utilisateur qui a créé le mouvement est obligatoire")]
    [StringLength(50, ErrorMessage = "Le nom de l'utilisateur ne peut pas dépasser 50 caractères")]
    [Display(Name = "Créé par")]
    public string? CreatedBy { get; set; }

    [ForeignKey("Order")]
    [Display(Name = "ID Commande")]
    public int? OrderId { get; set; }

    [ValidateNever]
    [Display(Name = "Commande")]
    public Order? Order { get; set; }

    [Required(ErrorMessage = "La date du mouvement est obligatoire")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Date du Mouvement")]
    public DateTime MovementDate { get; set; }

    [ValidateNever]
    [Display(Name = "Items du Mouvement de Stock")]
    public ICollection<StockMovementPerItem>? StockMovementPerItem { get; set; }
}