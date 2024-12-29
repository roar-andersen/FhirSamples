using Firely.Fhir.Validation;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Validation;



var resolver = new CachedResolver(
    new MultiResolver(
        new DirectorySource("profiles"),
        ZipSource.CreateValidationSource()));

var terminologyService = new LocalTerminologyService(resolver);

var validator = new Validator(resolver, terminologyService);

var medicationAdministration = new MedicationAdministration
{
    Id = "123",
    Status = MedicationAdministration.MedicationAdministrationStatusCodes.Completed,
    Medication = new ResourceReference { Reference = "Medication/123" },
    Subject = new ResourceReference { Reference = "Patient/123" },
    Effective = new Period { Start = "2021-01-01" }
};


var operationOutcome = validator.Validate(medicationAdministration, "http://hl7.no/fhir/ig/lmdi/StructureDefinition/lmdi-legemiddeladministrasjon");

System.Console.WriteLine(operationOutcome.ToJson(new FhirJsonSerializationSettings { Pretty = true }));
System.Console.ReadKey();


