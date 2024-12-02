
from abc import ABC, abstractmethod

# Interface for workflow steps
class IWorkflowStep(ABC):
    @abstractmethod
    def execute_step(self):
        pass

# Concrete step for validating orders
class ValidateOrder(IWorkflowStep):
    def execute_step(self):
        print("Validating Order...")

# Concrete step for processing payments
class ProcessPayment(IWorkflowStep):
    def execute_step(self):
        print("Processing Payment...")

# Concrete step for shipping orders
class ShipOrder(IWorkflowStep):
    def execute_step(self):
        print("Shipping Order...")

# Concrete step for generating invoices
class GenerateInvoice(IWorkflowStep):
    def execute_step(self):
        print("Generating Invoice...")

# Concrete step for sending invoices
class SendInvoice(IWorkflowStep):
    def execute_step(self):
        print("Sending Invoice to Customer...")

# Custom step for preparing a special order
class PrepareSpecialOrder(IWorkflowStep):
    def execute_step(self):
        print("Preparing Special Order...")

# Custom step for notifying customers
class NotifyCustomer(IWorkflowStep):
    def execute_step(self):
        print("Notifying Customer...")

# Abstract factory class for creating workflow steps
class WorkflowFactory(ABC):
    @abstractmethod
    def create_workflow_steps(self):
        pass

# Concrete factory for creating order processing workflow steps
class OrderProcessingFactory(WorkflowFactory):
    def create_workflow_steps(self):
        return [
            ValidateOrder(),
            ProcessPayment(),
            ShipOrder()
        ]

# Concrete factory for creating invoice processing workflow steps
class InvoiceProcessingFactory(WorkflowFactory):
    def create_workflow_steps(self):
        return [
            GenerateInvoice(),
            SendInvoice()
        ]

# Concrete factory for creating custom workflow steps
class CustomWorkflowFactory(WorkflowFactory):
    def create_workflow_steps(self):
        return [
            PrepareSpecialOrder(),
            NotifyCustomer()
        ]

# Class responsible for executing workflows using a specific workflow factory
class WorkflowProcessor:
    def __init__(self, factory):
        self.factory = factory

    def execute_workflow(self):
        steps = self.factory.create_workflow_steps()
        for step in steps:
            try:
                step.execute_step()
            except Exception as ex:
                print(f"Error executing step: {ex}")

# Main function for executing workflows
def main():
    # Execute Order Processing Workflow
    order_processor = WorkflowProcessor(OrderProcessingFactory())
    print("Executing Order Processing Workflow:")
    order_processor.execute_workflow()

    # Execute Invoice Processing Workflow
    invoice_processor = WorkflowProcessor(InvoiceProcessingFactory())
    print("\nExecuting Invoice Processing Workflow:")
    invoice_processor.execute_workflow()

    # Execute Custom Workflow
    custom_processor = WorkflowProcessor(CustomWorkflowFactory())
    print("\nExecuting Custom Workflow:")
    custom_processor.execute_workflow()

if __name__ == "__main__":
    main()
