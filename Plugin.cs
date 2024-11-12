using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DebugMethodPlugin
{
    [BepInPlugin("com.yourname.debugmethodplugin", "Debug Method Plugin", "1.0.0")]
    public class DebugMethodPlugin : BasePlugin
    {
        private static MethodInfo lastExecutedMethod;
        private static IntPtr lastExecutedMethodAddress;
        private static Dictionary<string, IntPtr> methodAddresses = new Dictionary<string, IntPtr>();
        private static string logFilePath = Path.Combine(Paths.PluginPath, "DebugMethodLog.txt");

        public override void Load()
        {
            Harmony harmony = new Harmony("com.yourname.debugmethodplugin");
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            // Initialize the log file
            File.WriteAllText(logFilePath, "Debug Method Plugin Log\n\n");

            // Scan for methods to hook (example for illustrative purposes)
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    if (method != null)
                    {
                        var methodPtr = method.MethodHandle.GetFunctionPointer();
                        methodAddresses[method.Name] = methodPtr;

                        // Patch the method with Harmony
                        var harmonyMethod = new HarmonyMethod(typeof(DebugMethodPlugin).GetMethod("MethodPrefix", BindingFlags.Static | BindingFlags.NonPublic));
                        harmony.Patch(method, prefix: harmonyMethod);
                    }
                }
            }
        }

        // Prefix method for Harmony to log the method calls
        private static void MethodPrefix(MethodBase __originalMethod)
        {
            lastExecutedMethod = __originalMethod;
            lastExecutedMethodAddress = methodAddresses[__originalMethod.Name];

            // Log the executed method and address
            string logEntry = $"Method executed: {__originalMethod.Name}, Address: {lastExecutedMethodAddress}\n";
            File.AppendAllText(logFilePath, logEntry);

            Console.WriteLine(logEntry);
        }

        // Event handler for process exit to log the last executed method
        private void OnProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("Process is exiting.");
            if (lastExecutedMethod != null)
            {
                string exitLog = $"Last executed method before exit: {lastExecutedMethod.Name}, Address: {lastExecutedMethodAddress}\n";
                File.AppendAllText(logFilePath, exitLog);
                Console.WriteLine(exitLog);
            }
        }

        // Event handler for unhandled exceptions to log the last executed method
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Unhandled exception occurred.");
            if (lastExecutedMethod != null)
            {
                string crashLog = $"Last executed method before crash: {lastExecutedMethod.Name}, Address: {lastExecutedMethodAddress}\n";
                File.AppendAllText(logFilePath, crashLog);
                Console.WriteLine(crashLog);
            }

            if (e.ExceptionObject is Exception ex)
            {
                string exceptionLog = $"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}\n";
                File.AppendAllText(logFilePath, exceptionLog);
                Console.WriteLine(exceptionLog);
            }
        }
    }
}
