open System

// Interface for workflow steps
type IWorkflowStep =
    abstract member ExecuteStep: unit -> unit

// Concrete step for validating orders
type ValidateOrder() =
    interface IWorkflowStep with
        member _.ExecuteStep() =
            printfn "Validating Order..."

// Concrete step for processing payments
type ProcessPayment() =
    interface IWorkflowStep with
        member _.ExecuteStep() =
            printfn "Processing Payment..."

// Concrete step for shipping orders
type ShipOrder() =
    interface IWorkflowStep with
        member _.ExecuteStep() =
            printfn "Shipping Order..."

// Concrete step for generating invoices
type GenerateInvoice() =
    interface IWorkflowStep with
        member _.ExecuteStep() =
            printfn "Generating Invoice..."

// Concrete step for sending invoices
type SendInvoice() =
    interface IWorkflowStep with
        member _.ExecuteStep() =
            printfn "Sending Invoice to Customer..."

// Custom step for preparing a special order
type PrepareSpecialOrder() =
    interface IWorkflowStep with
        member _.ExecuteStep() =
            printfn "Preparing Special Order..."

// Custom step for notifying customers
type NotifyCustomer() =
    interface IWorkflowStep with
        member _.ExecuteStep() =
            printfn "Notifying Customer..."

// Abstract factory for creating workflow steps
[<AbstractClass>]
type WorkflowFactory() =
    abstract CreateWorkflowSteps: unit -> IWorkflowStep list

// Concrete factory for creating order processing workflow steps
type OrderProcessingFactory() =
    inherit WorkflowFactory()
    override _.CreateWorkflowSteps() =
        [ ValidateOrder() :> IWorkflowStep
          ProcessPayment() :> IWorkflowStep
          ShipOrder() :> IWorkflowStep ]

// Concrete factory for creating invoice processing workflow steps
type InvoiceProcessingFactory() =
    inherit WorkflowFactory()
    override _.CreateWorkflowSteps() =
        [ GenerateInvoice() :> IWorkflowStep
          SendInvoice() :> IWorkflowStep ]

// Concrete factory for creating custom workflow steps
type CustomWorkflowFactory() =
    inherit WorkflowFactory()
    override _.CreateWorkflowSteps() =
        [ PrepareSpecialOrder() :> IWorkflowStep
          NotifyCustomer() :> IWorkflowStep ]

// Class responsible for executing workflows using a specific workflow factory
type WorkflowProcessor(factory: WorkflowFactory) =
    member _.ExecuteWorkflow() =
        let steps = factory.CreateWorkflowSteps()
        for step in steps do
            try
                step.ExecuteStep()
            with
            | ex -> printfn "Error executing step: %s" ex.Message

// Main function for executing workflows
[<EntryPoint>]
let main _ =
    // Execute Order Processing Workflow
    let orderProcessor = WorkflowProcessor(OrderProcessingFactory())
    printfn "Executing Order Processing Workflow:"
    orderProcessor.ExecuteWorkflow()

    // Execute Invoice Processing Workflow
    let invoiceProcessor = WorkflowProcessor(InvoiceProcessingFactory())
    printfn "\nExecuting Invoice Processing Workflow:"
    invoiceProcessor.ExecuteWorkflow()

    // Execute Custom Workflow
    let customProcessor = WorkflowProcessor(CustomWorkflowFactory())
    printfn "\nExecuting Custom Workflow:"
    customProcessor.ExecuteWorkflow()

    0 // Return an integer exit code
