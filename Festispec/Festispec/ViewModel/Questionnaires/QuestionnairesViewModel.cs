﻿using Festispec.Domain;
using Festispec.View.Questionnaires;
using Festispec.ViewModel.Questionnaires.Types;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.Questionnaires
{
    public class QuestionnairesViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionTypeViewModel> QuestionTypes { get; set; }

        public ObservableCollection<QuestionViewModel> Questions { get; set; }

        private QuestionViewModel _selectedQuestion;

        public QuestionViewModel SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                EditPage = _selectedQuestion?.GetEditPage;
                RaisePropertyChanged("EditPage");
                RaisePropertyChanged("SelectedQuestion");
                RaisePropertyChanged("GetVisibility");
            }
        }

        public string GetVisibility
        {
            get
            {
                if (SelectedQuestion == null) return "Hidden";
                else return "Visible";
            }
        }

        public void ChangeType(QuestionViewModel question)
        {
            var newVm = GetQuestionClass(question.ToModel());
            int index = Questions.IndexOf(question);
            Questions[index] = newVm;
            SelectedQuestion = newVm;

            RaisePropertyChanged("EditPage");
        }

        private Page _editPage;

        public Page EditPage
        {
            get { return _editPage; }
            set
            {
                _editPage = value;
                RaisePropertyChanged("EditPage");
            }
        }

        public ICommand AddQuestionCommand { get; set; }


        public QuestionnairesViewModel()
        {
            AddQuestionCommand = new RelayCommand(AddQuestion);

            using (var context = new FestispecEntities())
            {
                var questions = context.Questions.ToList()
                    .Select(q => GetQuestionClass(q));


                Questions = new ObservableCollection<QuestionViewModel>(questions);

                var questionTypes = context.Type_questions.ToList()
                    .Select(qt => new QuestionTypeViewModel(qt));

                QuestionTypes = new ObservableCollection<QuestionTypeViewModel>(questionTypes);
            }
        }

        private QuestionViewModel GetQuestionClass(Questions q)
        {
            switch (q.type_question)
            {
                case 1:
                    return new OpenQuestion(this, q);

                case 2:
                    return new MultipleChoiseQuestion(this, q);

                case 3:
                    return new SelectQuestion(this, q);

                default:
                    return null;
            }
        }

        private void AddQuestion()
        {
            var question = new Questions() { question = "New question", type_question = 2 };

            using (var context = new FestispecEntities())
            {
                context.Questions.Add(question);
                question.Type_questions = context.Type_questions.First(tq => tq.id == question.type_question);
                context.SaveChanges();
            }

            var newQuestion = GetQuestionClass(question);
            SelectedQuestion = newQuestion;
            Questions.Add(newQuestion);
        }
    }
}
