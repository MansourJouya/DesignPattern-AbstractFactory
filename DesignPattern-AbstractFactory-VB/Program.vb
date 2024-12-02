Imports System
Imports System.Collections.Generic

' Interface for workflow steps
Public Interface IWorkflowStep
    Sub ExecuteStep()
End Interface

' Concrete step for validating orders
Public Class ValidateOrder
    Implements IWorkflowStep

    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Validating Order...")
    End Sub
End Class

' Concrete step for processing payments
Public Class ProcessPayment
    Implements IWorkflowStep

    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Processing Payment...")
    End Sub
End Class

' Concrete step for shipping orders
Public Class ShipOrder
    Implements IWorkflowStep

    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Shipping Order...")
    End Sub
End Class

' Concrete step for generating invoices
Public Class GenerateInvoice
    Implements IWorkflowStep

    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Generating Invoice...")
    End Sub
End Class

' Concrete step for sending invoices
Public Class SendInvoice
    Implements IWorkflowStep

    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Sending Invoice to Customer...")
    End Sub
End Class

' Custom step for preparing a special order
Public Class PrepareSpecialOrder
    Implements IWorkflowStep

    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Preparing Special Order...")
    End Sub
End Class

' Custom step for notifying customers
Public Class NotifyCustomer
    Implements IWorkflowStep

    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Notifying Customer...")
    End Sub
End Class

' Abstract factory for creating workflow steps
Public MustInherit Class WorkflowFactory
    Public MustOverride Function CreateWorkflowSteps() As List(Of IWorkflowStep)
End Class

' Concrete factory for creating order processing workflow steps
Public Class OrderProcessingFactory
    Inherits WorkflowFactory

    Public Overrides Function CreateWorkflowSteps() As List(Of IWorkflowStep)
        Return New List(Of IWorkflowStep) From {
            New ValidateOrder(),
            New ProcessPayment(),
            New ShipOrder()
        }
    End Function
End Class

' Concrete factory for creating invoice processing workflow steps
Public Class InvoiceProcessingFactory
    Inherits WorkflowFactory

    Public Overrides Function CreateWorkflowSteps() As List(Of IWorkflowStep)
        Return New List(Of IWorkflowStep) From {
            New GenerateInvoice(),
            New SendInvoice()
        }
    End Function
End Class

' Concrete factory for creating custom workflow steps
Public Class CustomWorkflowFactory
    Inherits WorkflowFactory

    Public Overrides Function CreateWorkflowSteps() As List(Of IWorkflowStep)
        Return New List(Of IWorkflowStep) From {
            New PrepareSpecialOrder(),
            New NotifyCustomer()
        }
    End Function
End Class

' Class responsible for executing workflows using a specific workflow factory
Public Class WorkflowProcessor
    Private ReadOnly _factory As WorkflowFactory

    Public Sub New(factory As WorkflowFactory)
        _factory = factory
    End Sub

    Public Sub ExecuteWorkflow()
        Dim steps As List(Of IWorkflowStep) = _factory.CreateWorkflowSteps()
        ' Loop through each workflow step
        For Each workflowStepObj As Object In steps
            Dim workflowStep As IWorkflowStep = CType(workflowStepObj, IWorkflowStep) ' Cast to IWorkflowStep
            Try
                ' Execute each workflow step
                workflowStep.ExecuteStep()
            Catch ex As Exception
                ' Handle any exceptions
                Console.WriteLine($"Error executing step: {ex.Message}")
            End Try
        Next
    End Sub

End Class

' Client code for executing workflows
Module Program
    Sub Main()
        ' Execute Order Processing Workflow
        Console.WriteLine("Executing Order Processing Workflow:")
        Dim orderProcessor = New WorkflowProcessor(New OrderProcessingFactory())
        orderProcessor.ExecuteWorkflow()

        ' Execute Invoice Processing Workflow
        Console.WriteLine("Executing Invoice Processing Workflow:")
        Dim invoiceProcessor = New WorkflowProcessor(New InvoiceProcessingFactory())
        invoiceProcessor.ExecuteWorkflow()

        ' Execute Custom Workflow
        Console.WriteLine("Executing Custom Workflow:")
        Dim customProcessor = New WorkflowProcessor(New CustomWorkflowFactory())
        customProcessor.ExecuteWorkflow()
    End Sub
End Module
