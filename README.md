# MEPLog-
Develop a simple C# programming language with a simple to learn IDE
MEPLog+
![Screenshot (69)](https://github.com/user-attachments/assets/357f567c-499c-4a30-ac30-ff086b62eb74)

#### 1. **Project Overview**
The project involves the development of a simple Integrated Development Environment (IDE) designed to run and interpret basic code written by the user. The IDE includes features like line numbering, syntax highlighting, and the ability to execute code in a simple interpreted language. This environment supports basic programming constructs such as variable assignment, printing output, and mathematical operations. 

The project is built using **C#** and Windows Forms, leveraging `RichTextBox` for code editing and output display, along with a custom parser and interpreter for executing the user's code.

#### 2. **Key Features**

- **Code Editing**: A rich-text box is used to enter the code with basic syntax highlighting.
- **Line Numbers**: A separate panel shows line numbers alongside the code to help users identify and track their code easily.
- **Code Execution**: The user can write code and execute it through the IDE, and the output is displayed in a separate window.
- **Syntax Highlighting**: Keywords like `print` and mathematical operations (`+`, `-`, `*`, `/`) are highlighted to make the code more readable.
- **Error Handling**: The IDE includes basic error handling to show execution errors or issues like missing code when trying to run the program.
- **Output Display**: The output of the code execution is displayed in a read-only `RichTextBox`, where users can see the result or any errors.

#### 3. **User Interface (UI) Design**

The UI is composed of the following elements:

- **Code Editor**: A `RichTextBox` for writing the code, with the added functionality of displaying line numbers.
- **Output Panel**: Another `RichTextBox` used to display the result of code execution.
- **Line Number Panel**: A panel to the left of the code editor to show line numbers corresponding to each line of code.
- **Run Button**: A button at the top of the form to trigger the execution of the code.
- **Status Label**: A label to show the current status (Ready, Running, Error, etc.).

The layout is user-friendly, with components arranged in a way that supports efficient coding and immediate feedback.

#### 4. **Features and Functionalities**

1. **Code Editing and Execution**:
   - The user can write code directly into the code editor (`RichTextBox`).
   - The `Run` button triggers the code execution, and the result is displayed in the output panel.

2. **Line Numbering**:
   - The `Timer` periodically updates the line numbers in the panel to reflect the number of lines in the code editor.
   - This allows the user to easily reference specific lines of code, making debugging and navigation easier.

3. **Syntax Highlighting**:
   - Syntax highlighting is applied for keywords, numbers, and operators within the code editor. This makes the code more readable and intuitive.
   - Keywords like `print`, variable names, and mathematical operators are color-coded differently.

4. **Error Handling**:
   - If the user tries to run an empty program or enters invalid code, the IDE will show an error message and update the status to "Error."
   - Exception handling is implemented within the IDE to catch runtime errors and display error messages.

5. **Code Interpretation**:
   - The `Interpreter` class is responsible for parsing and executing the user's code.
   - A custom `Parser` converts the raw code into executable statements.
   - The `OutputCollector` captures the output of the executed statements, and it is displayed in the output panel.

#### 5. **Code Structure and Design**

- **MainForm**: The main user interface containing all the UI components, including code editor, output display, and line numbers. It also handles code execution via the `btnRun_Click` event handler.
  
- **Interpreter**: This class handles the execution of the code. It manages a `Context` where variables are stored and keeps track of the execution flow.

- **Parser**: The parser converts the user's code into a list of statements, such as assignments or print statements. It supports basic syntax parsing.

- **OutputCollector**: A helper class used to accumulate and display the output produced by the executed code.

- **ExpressionEvaluator**: This class is used to evaluate mathematical expressions written in the code. It supports basic arithmetic operations and handles tokenization, converting the expression into a postfix notation for evaluation.

- **Syntax Highlighting**: Simple highlighting is implemented through the use of `RichTextBox` formatting. Different keywords and operators are given unique colors to differentiate them from regular text.

#### 6. **Challenges Faced**

- **Line Number Update**: Keeping the line numbers up to date in real-time while the user is typing was a challenge. This was solved by using a `Timer` that updates the line numbers every 100 milliseconds.
  
- **Syntax Highlighting**: Implementing effective syntax highlighting required detecting keywords and operators in the code and applying the appropriate colors. The `RichTextBox` control's built-in features were utilized for this purpose.

- **Error Handling**: Ensuring that runtime errors are captured and displayed correctly was crucial for providing a user-friendly experience. Detailed error messages help users debug their code quickly.

#### 7. **Future Enhancements**

- **Additional Language Support**: Currently, the IDE only supports basic syntax and operations. Adding support for more complex constructs like loops, conditions (`if`, `else`), and functions would significantly enhance the functionality.
  
- **Advanced Syntax Highlighting**: More sophisticated syntax highlighting for various programming constructs (e.g., keywords, data types, functions) would make the editor more visually appealing and intuitive.
  
- **Code Autocompletion**: Adding code completion features would help users by suggesting possible keywords, variables, or functions as they type, speeding up the coding process.

- **Error Debugging Tools**: The IDE could integrate more advanced debugging tools, such as breakpoints, step-through execution, or variable watches, to help users debug their code interactively.

- **Project Support**: Future versions could allow users to save, open, and manage multiple code files or projects, making the IDE more practical for larger-scale development.

#### 8. **Conclusion**

This project has successfully created a simple IDE that allows users to write and execute basic code. By providing features like line numbers, syntax highlighting, and error handling, the IDE aims to make coding more accessible to beginners. The project serves as a starting point, with the potential for future enhancements and expansion to support more complex programming tasks. 

This IDE can be extended to support more advanced programming concepts and become a useful tool for both learning and development.
