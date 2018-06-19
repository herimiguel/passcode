using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace passcode.Controllers
{
    public class RandomController : Controller
    {
        public string genPasscode()
        {
            string digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random randNum = new Random();
            char[] passcodeArr = new char[10];
            for (int i=0; i<passcodeArr.Length; i++){
                passcodeArr[i] = digits[randNum.Next(digits.Length)];
            }
            string passcode = new String(passcodeArr);
            return passcode;  
        }


        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {            
            int? counter = HttpContext.Session.GetInt32("counter");
            if (counter == null)
            {
                counter = 1;
            } else {
                counter += 1;
            }

            HttpContext.Session.SetInt32("counter", (int)counter);
            ViewBag.counter = counter;
            ViewBag.passcode = genPasscode();
            return View("Index");
        }


        [HttpGet]
        [Route("generate")]
        public IActionResult generate()
        {
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("reset")]
        public IActionResult reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}