# Thread Pool, Async/Await, and Concurrency in C#

### Thread Pool
A pool of available threads managed by the .NET runtime. The thread pool dynamically assigns threads to tasks and receives threads back when they are released after completing their tasks.

### Thread
A unit of execution within an application. Each thread is responsible for processing a set of instructions and performing tasks such as handling user requests, executing methods, or processing data.

### `async` Keyword
Marks a method as asynchronous and allows the use of the `await` keyword inside it. When a method is marked with `async`, the compiler transforms it into a **state machine**. The state machine helps track the method’s execution, and its **MoveNext** function plays a key role in handling the continuation of the method when the `await` operation completes.

### `await` Keyword
Used to pause the execution of a method while awaiting the completion of an asynchronous task (such as an I/O or CPU-bound operation). When the program encounters an `await`, the **current thread is freed** to return to the thread pool, allowing it to be used for other tasks. The program does not wait for the awaited task to finish; instead, it moves on to execute other instructions.

### Thread Assignment After Completion
Once the asynchronous task is completed, the thread pool assigns a **new or any available thread** to continue executing the remaining instructions of the `async` method. This is handled through the state machine's **MoveNext** function.

### Asynchronous Programming
A programming model that allows tasks to run concurrently without blocking the execution of the entire program. It enables the application to continue processing while waiting for other operations (such as external API calls or database queries) to complete.

### Concurrency
The ability of a program to handle multiple tasks at the same time. In the context of asynchronous programming, concurrency allows the program to continue executing main instructions (on the main thread) while waiting for results from external resources. Once those results are available, the **next lines of instructions** are executed by any available thread from the pool.
