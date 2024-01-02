namespace Source.Scripts.test
{
    public class TestGovvHendler
    {
        private void Run()
        {
            TestGovv testGovv;
            testGovv = new TestGovv();
            testGovv.SendMessage();
            testGovv.Flex();
            Do(testGovv);
        }

        private void Do(ITestGovv testGovvv)
        {
            testGovvv.SendMessage();
        }
        public string DoSum(int a, int b)
        {
            var c = a + b;
            return $"{a} + {b} = {c}";
        }
    }
}