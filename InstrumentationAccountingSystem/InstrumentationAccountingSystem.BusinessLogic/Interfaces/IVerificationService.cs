using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentationAccountingSystem.Common.Dto;
using InstrumentationAccountingSystem.Models;

namespace InstrumentationAccountingSystem.BusinessLogic.Interfaces
{
    public interface IVerificationService
    {
        void Create(VerificationCreateDto verificationCreateDto);

        List<Verification> GetAll();

        void EditVerification(Verification verification);

        void DeleteVerificationById(int id);

        Verification GetVerificationById(int id);

        Verification? GetLastVerificationByInstrumentationId(int id);
    }
}
