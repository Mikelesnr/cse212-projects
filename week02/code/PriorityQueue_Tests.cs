using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add multiple items with different priorities and dequeue one
    // Expected Result: The item with the highest priority should be returned
    // Defect(s) Found: Original Dequeue logic skipped last item in list due to incorrect loop bounds
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 5);
        priorityQueue.Enqueue("Medium", 3);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("High", result); // Highest priority
    }

    [TestMethod]
    // Scenario: Add multiple items with the same highest priority, test tie-breaking
    // Expected Result: The earliest enqueued item (FIFO) with highest priority should be returned
    // Defect(s) Found: Original Dequeue logic favored later items with equal priority
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 5);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("First", result); // FIFO among equal priorities
    }

    [TestMethod]
    // Scenario: Dequeue from empty queue
    // Expected Result: An InvalidOperationException should be thrown
    // Defect(s) Found: No defect; just verifying exception handling works correctly
    public void TestPriorityQueue_EmptyQueueThrows()
    {
        var priorityQueue = new PriorityQueue();

        Assert.ThrowsException<InvalidOperationException>(() =>
        {
            priorityQueue.Dequeue();
        });
    }

    [TestMethod]
    // Scenario: Mixed priorities and orderings
    // Expected Result: Items dequeued in correct priority-first, then FIFO order
    // Defect(s) Found: Confirmed fix to loop bounds ensures correct selection
    public void TestPriorityQueue_MixedPriorityOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Zebra", 2);
        priorityQueue.Enqueue("Apple", 3);
        priorityQueue.Enqueue("Monkey", 2);
        priorityQueue.Enqueue("Banana", 3);

        var first = priorityQueue.Dequeue(); // Apple (highest, earliest)
        var second = priorityQueue.Dequeue(); // Banana (same priority, next)
        var third = priorityQueue.Dequeue(); // Zebra (next highest)
        var fourth = priorityQueue.Dequeue(); // Monkey

        Assert.AreEqual("Apple", first);
        Assert.AreEqual("Banana", second);
        Assert.AreEqual("Zebra", third);
        Assert.AreEqual("Monkey", fourth);
    }
}