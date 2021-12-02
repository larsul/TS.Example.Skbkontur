using SkbKontur.TypeScript.ContractGenerator;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using SkbKontur.TypeScript.ContractGenerator.CodeDom;
using System.Text;
using SkbKontur.TypeScript.ContractGenerator.Internals;
using System.Diagnostics;

namespace TS.Example.Skbkontur
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var p = new Program();
                p.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("a key to stop");
        }

        private void Run()
        {
            var pluginTypes = new List<Type> { typeof(Class2Typescript) };
            var generator = new TypeScriptGenerator(TypeScriptGenerationOptions.Default, CustomTypeGenerator.Null, new RootTypesProvider(pluginTypes.ToArray()));

            var unit = generator.Generate();
            unit.ToList().ForEach(a =>
            {
                a.Body.Cast<TypeScriptExportTypeStatement>().ToList().ForEach(b =>
                {
                    if (b.Declaration.Definition is JavaScriptEnumDefinition scriptDefinition)
                    {
                        scriptDefinition.Members.Cast<JavaScriptEnumMember>().ToList().ForEach(c =>
                        {
                            Debug.WriteLine($"{b.Declaration.Name} - {c.Name}");
                            c.Name = UppercaseFirst(c.Name);
                            Debug.WriteLine($"{b.Declaration.Name} - {c.Name}");
                        });
                    }
                    else
                    {
                        var declaration = b.Declaration.Definition as TypeScriptTypeDefintion;
                        declaration.Members.Cast<TypeScriptTypeMemberDeclaration>().ToList().ForEach(c =>
                        {
                            //Debug.WriteLine($"{b.Declaration.Name} - {c.Name}");
                            c.Name = UppercaseFirst(c.Name);
                            //Debug.WriteLine($"{b.Declaration.Name} - {c.Name}");
                        });
                    }
                    //"SkbKontur.TypeScript.ContractGenerator.CodeDom.JavaScriptEnumDefinition"

                });
            });
            //SkbKontur.TypeScript.ContractGenerator.CodeDom.TypeScriptTypeMemberDeclaration
            //var body = unit.First().Body.Cast<SkbKontur.TypeScript.ContractGenerator.CodeDom.TypeScriptExportTypeStatement>().ToList();
            //var declarations = body.Cast<SkbKontur.TypeScript.ContractGenerator.CodeDom.TypeScriptTypeDefintion>().
            //((SkbKontur.TypeScript.ContractGenerator.CodeDom.TypeScriptTypeDefintion)body.First().Declaration.Definition)
            //unit.First().Body.ForEach(_ => _.)

            var sb = new StringBuilder();

            var context = new DefaultCodeGenerationContext();
            unit.ToList().ForEach(_ =>
            {
                var code = _.GenerateCode(context);
                sb.Append(code.Replace(context.NewLine, ""));
            });
            Debug.WriteLine(sb.ToString().Replace("export ", "declare "));
        }

        private string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
