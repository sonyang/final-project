using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Models {
  public class IsbnConverter {

    public static string ConvertIsbn10(string t) {

      double checkDigit = 0;
      double sum = 0;

      int multiplier = 3;
      String x = "978" + t.Substring(0, 9);

      char[] code = x.ToCharArray();

      for (int i = 0; i < x.Length; i++) {
        multiplier = (multiplier == 3) ? 1 : 3;
        double num = Char.GetNumericValue(code[i]);
        sum += (num * multiplier);
      }

      checkDigit = 10 - (sum % 10);
      t = x + checkDigit;
      return t;
    }

    public static string ConvertIsbn13(string t) {

      double checkDigit = 0;
      double sum = 0;

      String x = t.Substring(3, 9);
      char[] code = x.ToCharArray();

      int multiplier = 10;

      for (int i = 0; i < code.Length; i++) {
        double num = Char.GetNumericValue(code[i]);
        sum = sum + (num * multiplier);
        multiplier--;
      }

      checkDigit = 11 - (sum % 11);
      t = x + checkDigit;

      return t;
    }

  }

}


