using SlugGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ECAdmin.Areas.Admin.Models;

namespace ECAdmin.Models.Helpers
{
    static class Slug
    {
        static Slug()
        {

        }
        public static string GetUniqSlug(string slug)
        {
            return slug.GenerateSlug();
        }

        public static string GetUniqSlug(string slug, List<string> slugs)
        {
            var newSlug = slug.GenerateSlug();

            var isUniq = slugs.Find(c => c == newSlug) ;
            if (isUniq == null)
                return newSlug;
            var i = 0;
            var nameNotUniq = true;
            var tmpSlug = newSlug;
            while (nameNotUniq)
            {
                ++i;
                if (slugs.Find(c => c == tmpSlug) == null)
                {
                    nameNotUniq = false;
                    slug = tmpSlug;
                }
                else
                {
                    tmpSlug = newSlug + "-" + i;
                }
            }
            return slug;
        }

        public static string foo = "bar";
    }
}
