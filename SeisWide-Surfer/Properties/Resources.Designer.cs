﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.36366
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SeisWide_Surfer.Properties {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SeisWide_Surfer.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Работа программы была завершена преждевременно. Устраните ошибки привязки SeisWide: записи одного номера волны с одинаковым номером трассы..
        /// </summary>
        internal static string msg_incorrect_txin {
            get {
                return ResourceManager.GetString("msg_incorrect_txin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на В выбранном каталоге были созданы подкаталоги для заголовков SeisWide, заголовков SeiSee и файлов корелляции формата &apos;tx.in&apos;, а также каталог для промежуточных результатов и каталог для выходных файлов интерполяции..
        /// </summary>
        internal static string msg_new_workspace {
            get {
                return ResourceManager.GetString("msg_new_workspace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Записать значения проекций в файл .out.
        /// </summary>
        internal static string with {
            get {
                return ResourceManager.GetString("with", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Для каждой тройки файлов
        ///&lt;заголовок SeiSee - заголовок SeisWide - .in&gt;
        ///записать значения проекций в файл .out.
        /// </summary>
        internal static string with_all {
            get {
                return ResourceManager.GetString("with_all", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Интерполировать все файлы .out.
        /// </summary>
        internal static string with_ip {
            get {
                return ResourceManager.GetString("with_ip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Записать привязку к трассе, взятую из заголовка SeisWide.
        /// </summary>
        internal static string without {
            get {
                return ResourceManager.GetString("without", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Для каждой пары файлов
        ///&lt;заголовок SeisWide - .in&gt;
        ///записать привязку к трассе.
        /// </summary>
        internal static string without_all {
            get {
                return ResourceManager.GetString("without_all", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Интерполировать все файлы привязки.
        /// </summary>
        internal static string without_ip {
            get {
                return ResourceManager.GetString("without_ip", resourceCulture);
            }
        }
    }
}
