
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StockManagement.Models;

public enum StockMovementStatus
{
    Incoming,   // Stock entrant
    Outgoing,   // Stock sortant
    Transfer,
    Merge,
    Delete
}

public class StockMovement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "ID Mouvement de Stock")]
    public int StockMovementId { get; set; }

    [Required(ErrorMessage = "Le type de mouvement est obligatoire")]
    [Display(Name = "Type de Mouvement")]
    public StockMovementStatus MovementType { get; set; }

    [Required(ErrorMessage = "L'utilisateur qui a créé le mouvement est obligatoire")]
    [StringLength(50, ErrorMessage = "Le nom de l'utilisateur ne peut pas dépasser 50 caractères")]
    [Display(Name = "Créé par")]
    public string? CreatedBy { get; set; }

    [Required(ErrorMessage = "La date du mouvement est obligatoire")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Date du Mouvement")]
    public DateTime MovementDate { get; set; }
    
    // Relation avec Le Block source
    [Required(ErrorMessage = "L'ID du block  source est obligatoire")]
    [ForeignKey("SourceBlock")]
    [Display(Name = "ID Block Source")]
    public int SourceProductBlockId { get; set; }

    [ValidateNever]
    [Required(ErrorMessage = "Le block source est obligatoire")]
    [Display(Name = "Block Source")]
    public ProductBlock SourceProductBlock { get; set; }

    // Relation avec le block de destination
    [Required(ErrorMessage = "L'ID du block de destination est obligatoire")]
    [ForeignKey("DestinationBlock")]
    [Display(Name = "ID Block de Destination")]
    public int DestinationProductBlockId { get; set; }
    [ValidateNever]
    [Required(ErrorMessage = "Le block de destination est obligatoire")]
    [Display(Name = "Block de Destination")]
    public ProductBlock DestinationProductBlock { get; set; }
    
    // Relation avec l'emplacement source
    [Required(ErrorMessage = "L'ID du Location  source est obligatoire")]
    [ForeignKey("SourceLocation")]
    [Display(Name = "ID Location Source")]
    public int SourceLocationId { get; set; }

    [ValidateNever]
    [Required(ErrorMessage = "Le Location source est obligatoire")]
    [Display(Name = "Location Source")]
    public Location SourceLocation { get; set; }

    // Relation avec l'emplacement de destination
    [Required(ErrorMessage = "L'ID du Location de destination est obligatoire")]
    [ForeignKey("DestinationLocation")]
    [Display(Name = "ID Location de Destination")]
    public int DestinationLocationId { get; set; }
    [ValidateNever]
    [Required(ErrorMessage = "Le block de destination est obligatoire")]
    [Display(Name = "Block de Destination")]
    public Location DestinationLocation { get; set; }
  
    [NotMapped]
    [ValidateNever]
    [Required(ErrorMessage = "Le produit doit être associé")]
    [Display(Name = "Produit")]
    public Product Product { get; set; }
    
    public int Quantity { get; set; }
    
    [ForeignKey("Order")]
    [Display(Name = "ID Commande")]
    public int? OrderId { get; set; }

    [ValidateNever]
    [Display(Name = "Commande")]
    public Order? Order { get; set; }
    
    [NotMapped]
    public ICollection<ProductItem> StockMovementItems { get; set; }
    
}