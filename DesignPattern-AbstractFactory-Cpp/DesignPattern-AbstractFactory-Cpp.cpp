#include <iostream>
#include <vector>
#include <memory>

// Interface for workflow steps
class IWorkflowStep {
public:
    virtual ~IWorkflowStep() = default;
    virtual void ExecuteStep() const = 0;
};

// Concrete step for validating orders
class ValidateOrder : public IWorkflowStep {
public:
    void ExecuteStep() const override {
        std::cout << "Validating Order..." << std::endl;
    }
};

// Concrete step for processing payments
class ProcessPayment : public IWorkflowStep {
public:
    void ExecuteStep() const override {
        std::cout << "Processing Payment..." << std::endl;
    }
};

// Concrete step for shipping orders
class ShipOrder : public IWorkflowStep {
public:
    void ExecuteStep() const override {
        std::cout << "Shipping Order..." << std::endl;
    }
};

// Concrete step for generating invoices
class GenerateInvoice : public IWorkflowStep {
public:
    void ExecuteStep() const override {
        std::cout << "Generating Invoice..." << std::endl;
    }
};

// Concrete step for sending invoices
class SendInvoice : public IWorkflowStep {
public:
    void ExecuteStep() const override {
        std::cout << "Sending Invoice to Customer..." << std::endl;
    }
};

// Custom step for preparing a special order
class PrepareSpecialOrder : public IWorkflowStep {
public:
    void ExecuteStep() const override {
        std::cout << "Preparing Special Order..." << std::endl;
    }
};

// Custom step for notifying customers
class NotifyCustomer : public IWorkflowStep {
public:
    void ExecuteStep() const override {
        std::cout << "Notifying Customer..." << std::endl;
    }
};

// Abstract factory class for creating workflow steps
class WorkflowFactory {
public:
    virtual ~WorkflowFactory() = default;
    virtual std::vector<std::shared_ptr<IWorkflowStep>> CreateWorkflowSteps() const = 0;
};

// Concrete factory for creating order processing workflow steps
class OrderProcessingFactory : public WorkflowFactory {
public:
    std::vector<std::shared_ptr<IWorkflowStep>> CreateWorkflowSteps() const override {
        return {
            std::make_shared<ValidateOrder>(),
            std::make_shared<ProcessPayment>(),
            std::make_shared<ShipOrder>()
        };
    }
};

// Concrete factory for creating invoice processing workflow steps
class InvoiceProcessingFactory : public WorkflowFactory {
public:
    std::vector<std::shared_ptr<IWorkflowStep>> CreateWorkflowSteps() const override {
        return {
            std::make_shared<GenerateInvoice>(),
            std::make_shared<SendInvoice>()
        };
    }
};

// Concrete factory for creating custom workflow steps
class CustomWorkflowFactory : public WorkflowFactory {
public:
    std::vector<std::shared_ptr<IWorkflowStep>> CreateWorkflowSteps() const override {
        return {
            std::make_shared<PrepareSpecialOrder>(),
            std::make_shared<NotifyCustomer>()
        };
    }
};

// Class responsible for executing workflows using a specific workflow factory
class WorkflowProcessor {
private:
    const WorkflowFactory& factory;

public:
    explicit WorkflowProcessor(const WorkflowFactory& factory) : factory(factory) {}

    void ExecuteWorkflow() const {
        auto steps = factory.CreateWorkflowSteps();
        for (const auto& step : steps) {
            try {
                step->ExecuteStep();
            }
            catch (const std::exception& ex) {
                std::cerr << "Error executing step: " << ex.what() << std::endl;
            }
        }
    }
};

// Main function for executing workflows
int main() {
    // Execute Order Processing Workflow
    OrderProcessingFactory orderFactory;
    WorkflowProcessor orderProcessor(orderFactory);
    std::cout << "Executing Order Processing Workflow:" << std::endl;
    orderProcessor.ExecuteWorkflow();

    // Execute Invoice Processing Workflow
    InvoiceProcessingFactory invoiceFactory;
    WorkflowProcessor invoiceProcessor(invoiceFactory);
    std::cout << "\nExecuting Invoice Processing Workflow:" << std::endl;
    invoiceProcessor.ExecuteWorkflow();

    // Execute Custom Workflow
    CustomWorkflowFactory customFactory;
    WorkflowProcessor customProcessor(customFactory);
    std::cout << "\nExecuting Custom Workflow:" << std::endl;
    customProcessor.ExecuteWorkflow();

    return 0;
}
