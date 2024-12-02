// Interface for workflow steps
class IWorkflowStep {
    executeStep() {
        throw new Error("Method 'executeStep()' must be implemented.");
    }
}

// Concrete steps
class ValidateOrder extends IWorkflowStep {
    executeStep() {
        console.log("Validating Order...");
    }
}

class ProcessPayment extends IWorkflowStep {
    executeStep() {
        console.log("Processing Payment...");
    }
}

class ShipOrder extends IWorkflowStep {
    executeStep() {
        console.log("Shipping Order...");
    }
}

class GenerateInvoice extends IWorkflowStep {
    executeStep() {
        console.log("Generating Invoice...");
    }
}

class SendInvoice extends IWorkflowStep {
    executeStep() {
        console.log("Sending Invoice to Customer...");
    }
}

class PrepareSpecialOrder extends IWorkflowStep {
    executeStep() {
        console.log("Preparing Special Order...");
    }
}

class NotifyCustomer extends IWorkflowStep {
    executeStep() {
        console.log("Notifying Customer...");
    }
}

// Abstract factory
class WorkflowFactory {
    createWorkflowSteps() {
        throw new Error("Method 'createWorkflowSteps()' must be implemented.");
    }
}

// Concrete factories
class OrderProcessingFactory extends WorkflowFactory {
    createWorkflowSteps() {
        return [
            new ValidateOrder(),
            new ProcessPayment(),
            new ShipOrder()
        ];
    }
}

class InvoiceProcessingFactory extends WorkflowFactory {
    createWorkflowSteps() {
        return [
            new GenerateInvoice(),
            new SendInvoice()
        ];
    }
}

class CustomWorkflowFactory extends WorkflowFactory {
    createWorkflowSteps() {
        return [
            new PrepareSpecialOrder(),
            new NotifyCustomer()
        ];
    }
}

// Workflow processor
class WorkflowProcessor {
    constructor(factory) {
        this.factory = factory;
    }

    executeWorkflow() {
        const steps = this.factory.createWorkflowSteps();
        steps.forEach(step => {
            try {
                step.executeStep();
            } catch (error) {
                console.error(`Error executing step: ${error.message}`);
            }
        });
    }
}

// Client code
function main() {
    console.log("Executing Order Processing Workflow:");
    const orderProcessor = new WorkflowProcessor(new OrderProcessingFactory());
    orderProcessor.executeWorkflow();

    console.log("\nExecuting Invoice Processing Workflow:");
    const invoiceProcessor = new WorkflowProcessor(new InvoiceProcessingFactory());
    invoiceProcessor.executeWorkflow();

    console.log("\nExecuting Custom Workflow:");
    const customProcessor = new WorkflowProcessor(new CustomWorkflowFactory());
    customProcessor.executeWorkflow();
}

main();
