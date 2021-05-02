//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Rosier.Blog.Web.Controllers;
//using Xunit;

//namespace Rosier.Blog.Web.Tests
//{
//    public class BlogControllerFactoryTests
//    {
//        private BlogControllerFactory factory; 

//        public BlogControllerFactoryTests()
//        {
//            factory = new BlogControllerFactory();
//        }

//        [Theory]
//        [InlineData("blog", typeof(BlogController))]
//        [InlineData("contact", typeof(ContactController))]
//        [InlineData("category", typeof(CategoryController))]
//        [InlineData("about", typeof(AboutController))]
//        [InlineData("error", typeof(ErrorController))]
//        //[InlineData("", typeof(ArgumentNullException), ExpectedException=typeof(ArgumentNullException))]
//        public void Create_BlogController(string controllerName, Type expectedtype)
//        {
//            var controller = factory.CreateController(new System.Web.Routing.RequestContext(), controllerName);

//            Assert.Equal(expectedtype, controller.GetType());
//        }
//    }
//}
